<%@ Page Language="C#" Trace="false" AutoEventWireup="false" EnableSessionState="false" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Collections" %>

<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%--
 * CKFinder
 * ========
 * http://cksource.com/ckfinder
 * Copyright (C) 2007-2015, CKSource - Frederico Knabben. All rights reserved.
 *
 * The software, this file and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.

 * Asp.net loopback file for Zip operations
--%>
<script runat="server">

	// Checks if the call to this page has been made from this same server.
	// In a shared hosting the call to GetTempPath might fail with a SecurityException
	private bool CheckIsLocalRequest()
	{
		// Check if the temporary file exists.
		string tempFile = Request[ "tmp" ];
		// if a temp path has been set use it, else try to get the system temp path
		string tempFolder  = ConfigurationSettings.AppSettings[ "CKFinderTempPath" ] ;
		if ( tempFolder == null || tempFolder.Length == 0)
		{
			try
			{
				tempFolder = System.IO.Path.GetTempPath() ;
			}
			catch ( System.Security.SecurityException ex )
			{
				Response.Write( "The security settings doesn't allow the automatic authentication between Asp and Asp.Net, please use the CKFinderTempPath setting" );
				Response.End();
			}
		}

		if ( tempFile != null && tempFile.Length > 0 )
			tempFile = System.IO.Path.Combine( tempFolder, tempFile + ".ckfindertemp") ;


		if ( tempFile == null || tempFile.Length == 0 || !System.IO.File.Exists( tempFile ) )
		{
			Response.StatusCode = 403;
			Response.Write( "<h1>403 - Forbidden</h1>" );
			//Response.Write( "<p>IsLocal: " + Request.IsLocal + "</p>" );
			//Response.Write( "<p>UserHostName: " + Request.UserHostName + "</p>" );
			//Response.Write( "<p>Remote: " + Request.ServerVariables[ "REMOTE_ADDR" ] + "</p>" );
			//Response.Write( "<p>Local: " + Request.ServerVariables[ "LOCAL_ADDR" ] + "</p>" );
			Response.End();
			return false;
		}

		return true;
	}

	protected override void OnLoad( EventArgs e )
	{
		if (!CheckIsLocalRequest())
			return;

		// Cleans the response buffer.
		Response.ClearHeaders();
		Response.Clear();

		// Prevent the browser from caching the result.
		Response.CacheControl = "no-cache";

		// Set the response format.
		Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
		Response.ContentType = "text/xml";

		XmlDocument _Xml = new XmlDocument();

		// Create the XML document header.
		_Xml.AppendChild( _Xml.CreateXmlDeclaration( "1.0", "utf-8", null ) );

		// Create the main "Connector" node.
		XmlNode _ConnectorNode = XmlUtil.AppendElement( _Xml, "Connector" );

		XmlNode oErrorNode = XmlUtil.AppendElement( _ConnectorNode, "Error" );

		try
		{
			string command = Request.QueryString[ "command" ];

			if ( command == null )
				ConnectorException.Throw( Errors.InvalidCommand );
			else
			{
				switch ( command )
				{
					case "IsEnabled" :
						break;

					//case "ValidateZip":
					//	ValidateZip();
					//	break;

					case "GetZipEntries":
						GetZipEntries(_ConnectorNode);
						break;

					case "ExtractZip":
						ExtractZip(_ConnectorNode);
						break;

					case "CreateZip":
						createZip();
						break;

					default:
						ConnectorException.Throw( Errors.InvalidCommand );
						break;
				}
			}

			XmlUtil.SetAttribute( oErrorNode, "number", "0" );
		}
		catch ( ConnectorException connectorException )
		{
			XmlUtil.SetAttribute( oErrorNode, "number", connectorException.Number.ToString() );
		}
		catch ( Exception ex )
		{
			XmlUtil.SetAttribute( oErrorNode, "number", Errors.Unknown.ToString() );
			XmlUtil.SetAttribute( oErrorNode, "text", ex.ToString() );
		}

		// Output the resulting XML.
		Response.Write( _Xml.OuterXml );

		Response.End();
	}

	private int ParseInt( Object value, int defaultValue )
	{
		if ( value == null )
			return defaultValue;

		return int.Parse( value.ToString() );
	}


	/**
	 * Reads the contents of a zip and appends them as children node of the connector
	 */
	private void GetZipEntries( XmlNode _ConnectorNode )
	{
		string ZipPath = HttpContext.Current.Request[ "ZipPath" ];
		if ( !System.IO.File.Exists( ZipPath ) )
			throw ( new System.IO.FileNotFoundException( "The specified file was not found.", ZipPath ) );

		// Create the "Response" node.
		XmlNode oResponseNode = XmlUtil.AppendElement( _ConnectorNode, "Response" );

		// Opens existing zip file
		ZipStorer zip = ZipStorer.Open(ZipPath, FileAccess.Read);

		// Read all directory contents
		List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

		foreach (ZipStorer.ZipFileEntry entry in dir)
		{
			XmlNode node = XmlUtil.AppendElement( oResponseNode, "File" );
			XmlUtil.SetAttribute( node, "name", entry.FilenameInZip );
			XmlUtil.SetAttribute( node, "size", entry.FileSize.ToString() );
		}
		zip.Close();
	}

	/**
	 * Extracts the requested files from a zip and appends the result as children nodes of the connector
	 * In "files" : key == file in zip, value == destination path
	 */
	private void ExtractZip( XmlNode _ConnectorNode )
	{
		string ZipPath = HttpContext.Current.Request[ "ZipPath" ];
		if ( !System.IO.File.Exists( ZipPath ) )
			throw ( new System.IO.FileNotFoundException( "The specified file was not found.", ZipPath ) );

		int count = ParseInt( HttpContext.Current.Request[ "files" ], 0);
		Dictionary<string, string> files = new  Dictionary<string, string>();

		for(int i=0; i<count; i++)
		{
			String name = HttpContext.Current.Request[ "name" + i.ToString() ];
			String path = HttpContext.Current.Request[ "path" + i.ToString() ];
			files.Add(name, path);
		}

		// Create the "Response" node.
		XmlNode oResponseNode = XmlUtil.AppendElement( _ConnectorNode, "Response" );

		// Opens existing zip file
		using( ZipStorer zip = ZipStorer.Open(ZipPath, FileAccess.Read))
		{
			// Read all directory contents
			List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

			foreach(ZipStorer.ZipFileEntry entry in dir)
			{
				if (files.ContainsKey(entry.FilenameInZip))
				{
					String path = files[ entry.FilenameInZip ];
					zip.ExtractFile(entry, path);
					XmlNode node = XmlUtil.AppendElement( oResponseNode, "File" );
					XmlUtil.SetAttribute( node, "name", entry.FilenameInZip );
					XmlUtil.SetAttribute( node, "result", "ok" );
					XmlUtil.SetAttribute( node, "path", path );
				}
			}
		}
	}

	/**
	 * Creates a zip with the requested files
	 */
	private void createZip()
	{
		string ZipPath = HttpContext.Current.Request[ "ZipPath" ];
		int count = ParseInt( HttpContext.Current.Request[ "files" ], 0);
		Dictionary<string, string> files = new  Dictionary<string, string>();

		for(int i=0; i<count; i++)
		{
			String file = HttpContext.Current.Request[ "file" + i.ToString() ];
			String path = HttpContext.Current.Request[ "path" + i.ToString() ];
			files.Add(file, path);
		}
		try
		{
			// Creates a new zip file
			using( ZipStorer zip = ZipStorer.Create( ZipPath, "" ))
			{
				zip.EncodeUTF8 = true;

				// Stores all the files into the zip file
				foreach (KeyValuePair<string, string> item in files)
				{
					if (item.Key.EndsWith("\\"))
						zip.AddStream( ZipStorer.Compression.Store, item.Value, null, File.GetLastWriteTime(item.Key), "");
					else
						zip.AddFile( ZipStorer.Compression.Deflate, item.Key, item.Value, "");
				}
			}
		}
		catch (Exception ex)
		{
			File.Delete(ZipPath);

			throw;
		}
	}

	/*******
	 * The following classes are pure copies of the original code of CKFinder for ASP.NET.
	 */

	/**
	 *  START ConnectorException.cs
	 */
	internal class ConnectorException : Exception
	{
		private int _Number;

		public ConnectorException( int number )
			: base()
		{
			_Number = number;
		}

		public int Number
		{
			get
			{
				return _Number;
			}
		}

		public static void Throw( int errorNumber )
		{
			throw new ConnectorException( errorNumber );
		}
	}
	/**
	 *  END ConnectorException.cs
	 */

	/**
	 *  START Errors.cs
	 */
	internal class Errors
	{
		public const int None = 0;
		public const int CustomError = 1;
		public const int InvalidCommand = 10;
		public const int TypeNotSpecified = 11;
		public const int InvalidType = 12;
		public const int InvalidName = 102;
		public const int Unauthorized = 103;
		public const int AccessDenied = 104;
		public const int InvalidRequest = 109;
		public const int Unknown = 110;
		public const int AlreadyExist = 115;
		public const int FolderNotFound = 116;
		public const int FileNotFound = 117;
		public const int UploadedFileRenamed = 201;
		public const int UploadedInvalid = 202;
		public const int UploadedTooBig = 203;
		public const int UploadedCorrupt = 204;
		public const int UploadedNoTmpDir = 205;
		public const int UploadedWrongHtmlFile = 206;
		public const int ConnectorDisabled = 500;
		public const int ThumbnailsDisabled = 501;
	}
	/**
	 *  END Errors.cs
	 */

	/**
	 *  START XmlUtil.cs
	 */
	internal sealed class XmlUtil
	{
		private XmlUtil()
		{ }

		public static XmlNode AppendElement( XmlNode node, string newElementName )
		{
			return AppendElement( node, newElementName, null );
		}

		public static XmlNode AppendElement( XmlNode node, string newElementName, string innerValue )
		{
			XmlNode oNode;

			if ( node is XmlDocument )
				oNode = node.AppendChild( ( (XmlDocument)node ).CreateElement( newElementName ) );
			else
				oNode = node.AppendChild( node.OwnerDocument.CreateElement( newElementName ) );

			if ( innerValue != null )
				oNode.AppendChild( node.OwnerDocument.CreateTextNode( innerValue ) );

			return oNode;
		}

		public static XmlAttribute CreateAttribute( XmlDocument xmlDocument, string name, string value )
		{
			XmlAttribute oAtt = xmlDocument.CreateAttribute( name );
			oAtt.Value = value;
			return oAtt;
		}

		public static void SetAttribute( XmlNode node, string attributeName, string attributeValue )
		{
			if ( node.Attributes[ attributeName ] != null )
				node.Attributes[ attributeName ].Value = attributeValue;
			else
				node.Attributes.Append( CreateAttribute( node.OwnerDocument, attributeName, attributeValue ) );
		}

		public static string GetAttribute( XmlNode node, string attributeName, string defaultValue )
		{
			XmlAttribute att = node.Attributes[ attributeName ];
			if ( att != null )
				return att.Value;
			else
				return defaultValue;
		}

		public static string GetNodeValue( XmlNode parentNode, string nodeXPath, string defaultValue )
		{
			XmlNode node = parentNode.SelectSingleNode( nodeXPath );
			if ( node != null )
				return node.Value;
			else
				return defaultValue;
		}
	}
	/**
	 *  END XmlUtil.cs
	 */



/*
	Includes patch http://www.codeplex.com/Download?ProjectName=zipstorer&DownloadId=232503 for better support of empty folders
	http://zipstorer.codeplex.com/workitem/5261
*/
// ZipStorer, by Jaime Olivares
// Website: zipstorer.codeplex.com
// Version: 2.35 (March 14, 2010)

//namespace System.IO.Compression
//{
    /// <summary>
    /// Unique class for compression/decompression file. Represents a Zip file.
    /// </summary>
    public class ZipStorer : IDisposable
    {
        /// <summary>
        /// Compression method enumeration
        /// </summary>
        public enum Compression : ushort {
            /// <summary>Uncompressed storage</summary>
            Store = 0,
            /// <summary>Deflate compression method</summary>
            Deflate = 8 }

        /// <summary>
        /// Represents an entry in Zip file directory
        /// </summary>
        public struct ZipFileEntry
        {
            /// <summary>Compression method</summary>
            public Compression Method;
            /// <summary>Full path and filename as stored in Zip</summary>
            public string FilenameInZip;
            /// <summary>Original file size</summary>
            public uint FileSize;
            /// <summary>Compressed file size</summary>
            public uint CompressedSize;
            /// <summary>Offset of header information inside Zip storage</summary>
            public uint HeaderOffset;
            /// <summary>Offset of file inside Zip storage</summary>
            public uint FileOffset;
            /// <summary>Size of header information</summary>
            public uint HeaderSize;
            /// <summary>32-bit checksum of entire file</summary>
            public uint Crc32;
            /// <summary>Last modification time of file</summary>
            public DateTime ModifyTime;
            /// <summary>User comment for file</summary>
            public string Comment;
            /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
            public bool EncodeUTF8;

            /// <summary>True if filename ends with slash</summary>
            public bool IsDirectory()
            {
                return FilenameInZip.EndsWith("/");
            }

            /// <summary>Overriden method</summary>
            /// <returns>Filename in Zip</returns>
            public override string ToString()
            {
                return this.FilenameInZip;
            }
        }

        #region Public fields
        /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
        public bool EncodeUTF8 = false;
        /// <summary>Force deflate algotithm even if it inflates the stored file. Off by default.</summary>
        public bool ForceDeflating = false;
        #endregion

        #region Private fields
        // List of files to store
        private List<ZipFileEntry> Files = new List<ZipFileEntry>();
        // Filename of storage file
        private string FileName;
        // Stream object of storage file
        private Stream ZipFileStream;
        // General comment
        private string Comment = "";
        // Central dir image
        private byte[] CentralDirImage = null;
        // Existing files in zip
        private ushort ExistingFiles = 0;
        // File access for Open method
        private FileAccess Access;
        // Static CRC32 Table
        private static UInt32[] CrcTable = null;
        // Default filename encoder
        private static Encoding DefaultEncoding = Encoding.GetEncoding(437);
        #endregion

        #region Public methods
        // Static constructor. Just invoked once in order to create the CRC32 lookup table.
        static ZipStorer()
        {
            // Generate CRC32 table
            CrcTable = new UInt32[256];
            for (int i = 0; i < CrcTable.Length; i++)
            {
                UInt32 c = (UInt32)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((c & 1) != 0)
                        c = 3988292384 ^ (c >> 1);
                    else
                        c >>= 1;
                }
                CrcTable[i] = c;
            }
        }
        /// <summary>
        /// Method to create a new storage file
        /// </summary>
        /// <param name="_filename">Full path of Zip file to create</param>
        /// <param name="_comment">General comment for Zip file</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Create(string _filename, string _comment)
        {
            Stream stream = new FileStream(_filename, FileMode.Create, FileAccess.ReadWrite);

            ZipStorer zip = Create(stream, _comment);
            zip.Comment = _comment;
            zip.FileName = _filename;

            return zip;
        }
        /// <summary>
        /// Method to create a new zip storage in a stream
        /// </summary>
        /// <param name="_stream"></param>
        /// <param name="_comment"></param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Create(Stream _stream, string _comment)
        {
            ZipStorer zip = new ZipStorer();
            zip.Comment = _comment;
            zip.ZipFileStream = _stream;
            zip.Access = FileAccess.Write;

            return zip;
        }
        /// <summary>
        /// Method to open an existing storage file
        /// </summary>
        /// <param name="_filename">Full path of Zip file to open</param>
        /// <param name="_access">File access mode as used in FileStream constructor</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Open(string _filename, FileAccess _access)
        {
            Stream stream = (Stream)new FileStream(_filename, FileMode.Open, _access == FileAccess.Read ? FileAccess.Read : FileAccess.ReadWrite);

            ZipStorer zip = Open(stream, _access);
            zip.FileName = _filename;

            return zip;
        }
        /// <summary>
        /// Method to open an existing storage from stream
        /// </summary>
        /// <param name="_stream">Already opened stream with zip contents</param>
        /// <param name="_access">File access mode for stream operations</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Open(Stream _stream, FileAccess _access)
        {
            if (!_stream.CanSeek && _access != FileAccess.Read)
                throw new InvalidOperationException("Stream cannot seek");

            ZipStorer zip = new ZipStorer();
            //zip.FileName = _filename;
            zip.ZipFileStream = _stream;
            zip.Access = _access;

            if (zip.ReadFileInfo())
                return zip;

            throw new System.IO.InvalidDataException();
        }
        /// <summary>
        /// Add full contents of a file into the Zip storage
        /// </summary>
        /// <param name="_method">Compression method</param>
        /// <param name="_pathname">Full path of file to add to Zip storage</param>
        /// <param name="_filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="_comment">Comment for stored file</param>
        public void AddFile(Compression _method, string _pathname, string _filenameInZip, string _comment)
        {
            if (Access == FileAccess.Read)
                throw new InvalidOperationException("Writing is not alowed");

            FileStream stream = new FileStream(_pathname, FileMode.Open, FileAccess.Read);
            AddStream(_method, _filenameInZip, stream, File.GetLastWriteTime(_pathname), _comment);
            stream.Close();
        }

        /// <summary>
        /// Add full contents of a directory into the Zip storage
        /// </summary>
        /// <param name="_method">Compression method</param>
        /// <param name="_pathname">Full path of directory to add to Zip storage</param>
        /// <param name="_pathnameInZip">Path name as desired in Zip directory</param>
        /// <param name="_comment">Comment for stored directory</param>
        public void AddDirectory(Compression _method, string _pathname, string _pathnameInZip, string _comment)
        {
            if (Access == FileAccess.Read)
                throw new InvalidOperationException("Writing is not allowed");

            string foldername;
            int pos = _pathname.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos >= 0)
            {
                foldername = _pathname.Remove(0, pos + 1);
            }
            else
            {
                foldername = _pathname;
            }

            if (_pathnameInZip != null && _pathnameInZip != "")
            {
                foldername = _pathnameInZip + foldername;
            }

            if (!foldername.EndsWith("/"))
            {
                foldername = foldername + "/";
            }

            AddStream(_method, foldername, null, File.GetLastWriteTime(_pathname), _comment);

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(_pathname);
            foreach (string fileName in fileEntries)
            {
                AddFile(_method, fileName, foldername + Path.GetFileName(fileName), "");
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(_pathname);
            foreach (string subdirectory in subdirectoryEntries)
            {
                AddDirectory(_method, subdirectory, foldername, "");
            }
        }

        /// <summary>
        /// Add full contents of a stream into the Zip storage
        /// </summary>
        /// <param name="_method">Compression method</param>
        /// <param name="_filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="_source">Stream object containing the data to store in Zip</param>
        /// <param name="_modTime">Modification time of the data to store</param>
        /// <param name="_comment">Comment for stored file</param>
        public void AddStream(Compression _method, string _filenameInZip, Stream _source, DateTime _modTime, string _comment)
        {
            if (Access == FileAccess.Read)
                throw new InvalidOperationException("Writing is not alowed");

            long offset;
            if (this.Files.Count==0)
                offset = 0;
            else
            {
                ZipFileEntry last = this.Files[this.Files.Count-1];
                offset = last.HeaderOffset + last.HeaderSize;
            }

            // Prepare the fileinfo
            ZipFileEntry zfe = new ZipFileEntry();
            zfe.Method = _method;
            zfe.EncodeUTF8 = this.EncodeUTF8;
            zfe.FilenameInZip = NormalizedFilename(_filenameInZip);
            zfe.Comment = (_comment == null ? "" : _comment);

            // Even though we write the header now, it will have to be rewritten, since we don't know compressed size or crc.
            zfe.Crc32 = 0;  // to be updated later
            zfe.HeaderOffset = (uint)this.ZipFileStream.Position;  // offset within file of the start of this local record
            zfe.ModifyTime = _modTime;

            // Write local header
            WriteLocalHeader(ref zfe);
            zfe.FileOffset = (uint)this.ZipFileStream.Position;

            // Write file to zip (store)
			if (_source != null)
			{
				Store(ref zfe, _source);
				_source.Close();
			}

            this.UpdateCrcAndSizes(ref zfe);

            Files.Add(zfe);
        }
        /// <summary>
        /// Updates central directory (if pertinent) and close the Zip storage
        /// </summary>
        /// <remarks>This is a required step, unless automatic dispose is used</remarks>
        public void Close()
        {
            if (this.Access != FileAccess.Read)
            {
                uint centralOffset = (uint)this.ZipFileStream.Position;
                uint centralSize = 0;

                if (this.CentralDirImage != null)
                    this.ZipFileStream.Write(CentralDirImage, 0, CentralDirImage.Length);

                for (int i = 0; i < Files.Count; i++)
                {
                    long pos = this.ZipFileStream.Position;
                    this.WriteCentralDirRecord(Files[i]);
                    centralSize += (uint)(this.ZipFileStream.Position - pos);
                }

                if (this.CentralDirImage != null)
                    this.WriteEndRecord(centralSize + (uint)CentralDirImage.Length, centralOffset);
                else
                    this.WriteEndRecord(centralSize, centralOffset);
            }

            if (this.ZipFileStream != null)
            {
                this.ZipFileStream.Flush();
                this.ZipFileStream.Dispose();
                this.ZipFileStream = null;
            }
        }
        /// <summary>
        /// Read all the file records in the central directory
        /// </summary>
        /// <returns>List of all entries in directory</returns>
        public List<ZipFileEntry> ReadCentralDir()
        {
            if (this.CentralDirImage == null)
                throw new InvalidOperationException("Central directory currently does not exist");

            List<ZipFileEntry> result = new List<ZipFileEntry>();

            for (int pointer = 0; pointer < this.CentralDirImage.Length; )
            {
                uint signature = BitConverter.ToUInt32(CentralDirImage, pointer);
                if (signature != 0x02014b50)
                    break;

                bool encodeUTF8 = (BitConverter.ToUInt16(CentralDirImage, pointer + 8) & 0x0800) != 0;
                ushort method = BitConverter.ToUInt16(CentralDirImage, pointer + 10);
                uint modifyTime = BitConverter.ToUInt32(CentralDirImage, pointer + 12);
                uint crc32 = BitConverter.ToUInt32(CentralDirImage, pointer + 16);
                uint comprSize = BitConverter.ToUInt32(CentralDirImage, pointer + 20);
                uint fileSize = BitConverter.ToUInt32(CentralDirImage, pointer + 24);
                ushort filenameSize = BitConverter.ToUInt16(CentralDirImage, pointer + 28);
                ushort extraSize = BitConverter.ToUInt16(CentralDirImage, pointer + 30);
                ushort commentSize = BitConverter.ToUInt16(CentralDirImage, pointer + 32);
                uint headerOffset = BitConverter.ToUInt32(CentralDirImage, pointer + 42);
                uint headerSize = (uint)( 46 + filenameSize + extraSize + commentSize);

                Encoding encoder = encodeUTF8 ? Encoding.UTF8 : DefaultEncoding;

                ZipFileEntry zfe = new ZipFileEntry();
                zfe.Method = (Compression)method;
                zfe.FilenameInZip = encoder.GetString(CentralDirImage, pointer + 46, filenameSize);
                zfe.FileOffset = GetFileOffset(headerOffset);
                zfe.FileSize = fileSize;
                zfe.CompressedSize = comprSize;
                zfe.HeaderOffset = headerOffset;
                zfe.HeaderSize = headerSize;
                zfe.Crc32 = crc32;
                zfe.ModifyTime = DosTimeToDateTime(modifyTime);
                if (commentSize > 0)
                    zfe.Comment = encoder.GetString(CentralDirImage, pointer + 46 + filenameSize + extraSize, commentSize);

                result.Add(zfe);
                pointer += (46 + filenameSize + extraSize + commentSize);
            }

            return result;
        }
        /// <summary>
        /// Copy the contents of a stored file into a physical file
        /// </summary>
        /// <param name="_zfe">Entry information of file to extract</param>
        /// <param name="_filename">Name of file to store uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry _zfe, string _filename)
        {
            // Make sure the parent directory exist
            string path = System.IO.Path.GetDirectoryName(_filename);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // Check it is directory. If so, do nothing
            if (Directory.Exists(_filename))
                return true;

            Stream output = new FileStream(_filename, FileMode.Create, FileAccess.Write);
            bool result = ExtractFile(_zfe, output);
            if (result)
                output.Close();

            File.SetCreationTime(_filename, _zfe.ModifyTime);
            File.SetLastWriteTime(_filename, _zfe.ModifyTime);

            return result;
        }
        /// <summary>
        /// Copy the contents of a stored file into an opened stream
        /// </summary>
        /// <param name="_zfe">Entry information of file to extract</param>
        /// <param name="_stream">Stream to store the uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry _zfe, Stream _stream)
        {
            if (!_stream.CanWrite)
                throw new InvalidOperationException("Stream cannot be written");

            // check signature
            byte[] signature = new byte[4];
            this.ZipFileStream.Seek(_zfe.HeaderOffset, SeekOrigin.Begin);
            this.ZipFileStream.Read(signature, 0, 4);
            if (BitConverter.ToUInt32(signature, 0) != 0x04034b50)
                return false;

            // Select input stream for inflating or just reading
            Stream inStream;
            if (_zfe.Method == Compression.Store)
                inStream = this.ZipFileStream;
            else if (_zfe.Method == Compression.Deflate)
                inStream = new DeflateStream(this.ZipFileStream, CompressionMode.Decompress, true);
            else
                return false;

            // Buffered copy
            byte[] buffer = new byte[16384];
            this.ZipFileStream.Seek(_zfe.FileOffset, SeekOrigin.Begin);
            uint bytesPending = _zfe.FileSize;
            while (bytesPending > 0)
            {
                int bytesRead = inStream.Read(buffer, 0, (int)Math.Min(bytesPending, buffer.Length));
                _stream.Write(buffer, 0, bytesRead);
                bytesPending -= (uint)bytesRead;
            }
            _stream.Flush();

            if (_zfe.Method == Compression.Deflate)
                inStream.Dispose();
            return true;
        }
        /// <summary>
        /// Removes one of many files in storage. It creates a new Zip file.
        /// </summary>
        /// <param name="_zip">Reference to the current Zip object</param>
        /// <param name="_zfes">List of Entries to remove from storage</param>
        /// <returns>True if success, false if not</returns>
        /// <remarks>This method only works for storage of type FileStream</remarks>
        public static bool RemoveEntries(ref ZipStorer _zip, List<ZipFileEntry> _zfes)
        {
            if (!(_zip.ZipFileStream is FileStream))
                throw new InvalidOperationException("RemoveEntries is allowed just over streams of type FileStream");


            //Get full list of entries
            List<ZipFileEntry> fullList = _zip.ReadCentralDir();

            //In order to delete we need to create a copy of the zip file excluding the selected items
            string tempZipName = Path.GetTempFileName();
            string tempEntryName = Path.GetTempFileName();

            try
            {
                ZipStorer tempZip = ZipStorer.Create(tempZipName, string.Empty);

                foreach (ZipFileEntry zfe in fullList)
                {
                    if (!_zfes.Contains(zfe))
                    {
                        if (_zip.ExtractFile(zfe, tempEntryName))
                        {
                            tempZip.AddFile(zfe.Method, tempEntryName, zfe.FilenameInZip, zfe.Comment);
                        }
                    }
                }
                _zip.Close();
                tempZip.Close();

                File.Delete(_zip.FileName);
                File.Move(tempZipName, _zip.FileName);

                _zip = ZipStorer.Open(_zip.FileName, _zip.Access);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (File.Exists(tempZipName))
                    File.Delete(tempZipName);
                if (File.Exists(tempEntryName))
                    File.Delete(tempEntryName);
            }
            return true;
        }
        #endregion

        #region Private methods
        // Calculate the file offset by reading the corresponding local header
        private uint GetFileOffset(uint _headerOffset)
        {
            byte[] buffer = new byte[2];

            this.ZipFileStream.Seek(_headerOffset + 26, SeekOrigin.Begin);
            this.ZipFileStream.Read(buffer, 0, 2);
            ushort filenameSize = BitConverter.ToUInt16(buffer, 0);
            this.ZipFileStream.Read(buffer, 0, 2);
            ushort extraSize = BitConverter.ToUInt16(buffer, 0);

            return (uint)(30 + filenameSize + extraSize + _headerOffset);
        }
        /* Local file header:
            local file header signature     4 bytes  (0x04034b50)
            version needed to extract       2 bytes
            general purpose bit flag        2 bytes
            compression method              2 bytes
            last mod file time              2 bytes
            last mod file date              2 bytes
            crc-32                          4 bytes
            compressed size                 4 bytes
            uncompressed size               4 bytes
            filename length                 2 bytes
            extra field length              2 bytes

            filename (variable size)
            extra field (variable size)
        */
        private void WriteLocalHeader(ref ZipFileEntry _zfe)
        {
            long pos = this.ZipFileStream.Position;
            Encoding encoder = _zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(_zfe.FilenameInZip);

            this.ZipFileStream.Write(new byte[] { 80, 75, 3, 4, 20, 0}, 0, 6); // No extra header
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)(_zfe.EncodeUTF8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);  // zipping method
            this.ZipFileStream.Write(BitConverter.GetBytes(DateTimeToDosTime(_zfe.ModifyTime)), 0, 4); // zipping date and time
            this.ZipFileStream.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 12); // unused CRC, un/compressed size, updated later
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // filename length
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length

            this.ZipFileStream.Write(encodedFilename, 0, encodedFilename.Length);
            _zfe.HeaderSize = (uint)(this.ZipFileStream.Position - pos);
        }
        /* Central directory's File header:
            central file header signature   4 bytes  (0x02014b50)
            version made by                 2 bytes
            version needed to extract       2 bytes
            general purpose bit flag        2 bytes
            compression method              2 bytes
            last mod file time              2 bytes
            last mod file date              2 bytes
            crc-32                          4 bytes
            compressed size                 4 bytes
            uncompressed size               4 bytes
            filename length                 2 bytes
            extra field length              2 bytes
            file comment length             2 bytes
            disk number start               2 bytes
            internal file attributes        2 bytes
            external file attributes        4 bytes
            relative offset of local header 4 bytes

            filename (variable size)
            extra field (variable size)
            file comment (variable size)
        */
        private void WriteCentralDirRecord(ZipFileEntry _zfe)
        {
            Encoding encoder = _zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(_zfe.FilenameInZip);
            byte[] encodedComment = encoder.GetBytes(_zfe.Comment);

            this.ZipFileStream.Write(new byte[] { 80, 75, 1, 2, 23, 0xB, 20, 0 }, 0, 8);
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)(_zfe.EncodeUTF8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);  // zipping method
            this.ZipFileStream.Write(BitConverter.GetBytes(DateTimeToDosTime(_zfe.ModifyTime)), 0, 4);  // zipping date and time
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.Crc32), 0, 4); // file CRC
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.CompressedSize), 0, 4); // compressed file size
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.FileSize), 0, 4); // uncompressed file size
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // Filename in zip
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);

            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // disk=0
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // file type: binary
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // Internal file attributes
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)0x8100), 0, 2); // External file attributes (normal/readable)
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.HeaderOffset), 0, 4);  // Offset of header

            this.ZipFileStream.Write(encodedFilename, 0, encodedFilename.Length);
            this.ZipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }
        /* End of central dir record:
            end of central dir signature    4 bytes  (0x06054b50)
            number of this disk             2 bytes
            number of the disk with the
            start of the central directory  2 bytes
            total number of entries in
            the central dir on this disk    2 bytes
            total number of entries in
            the central dir                 2 bytes
            size of the central directory   4 bytes
            offset of start of central
            directory with respect to
            the starting disk number        4 bytes
            zipfile comment length          2 bytes
            zipfile comment (variable size)
        */
        private void WriteEndRecord(uint _size, uint _offset)
        {
            Encoding encoder = this.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedComment = encoder.GetBytes(this.Comment);

            this.ZipFileStream.Write(new byte[] { 80, 75, 5, 6, 0, 0, 0, 0 }, 0, 8);
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)Files.Count+ExistingFiles), 0, 2);
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)Files.Count+ExistingFiles), 0, 2);
            this.ZipFileStream.Write(BitConverter.GetBytes(_size), 0, 4);
            this.ZipFileStream.Write(BitConverter.GetBytes(_offset), 0, 4);
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);
            this.ZipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }
        // Copies all source file into storage file
        private void Store(ref ZipFileEntry _zfe, Stream _source)
        {
            byte[] buffer = new byte[16384];
            int bytesRead;
            uint totalRead = 0;
            Stream outStream;

            long posStart = this.ZipFileStream.Position;
            long sourceStart = _source.Position;

            if (_zfe.Method == Compression.Store)
                outStream = this.ZipFileStream;
            else
                outStream = new DeflateStream(this.ZipFileStream, CompressionMode.Compress, true);

            _zfe.Crc32 = 0 ^ 0xffffffff;

            do
            {
                bytesRead = _source.Read(buffer, 0, buffer.Length);
                totalRead += (uint)bytesRead;
                if (bytesRead > 0)
                {
                    outStream.Write(buffer, 0, bytesRead);

                    for (uint i = 0; i < bytesRead; i++)
                    {
                        _zfe.Crc32 = ZipStorer.CrcTable[(_zfe.Crc32 ^ buffer[i]) & 0xFF] ^ (_zfe.Crc32 >> 8);
                    }
                }
            } while (bytesRead == buffer.Length);
            outStream.Flush();

            if (_zfe.Method == Compression.Deflate)
                outStream.Dispose();

            _zfe.Crc32 ^= 0xffffffff;
            _zfe.FileSize = totalRead;
            _zfe.CompressedSize = (uint)(this.ZipFileStream.Position - posStart);

            // Verify for real compression
            if (_zfe.Method == Compression.Deflate && !this.ForceDeflating && _source.CanSeek && _zfe.CompressedSize >= _zfe.FileSize)
            {
                // Start operation again with Store algorithm
                _zfe.Method = Compression.Store;
                this.ZipFileStream.Position = posStart;
                this.ZipFileStream.SetLength(posStart);
                _source.Position = sourceStart;
                this.Store(ref _zfe, _source);
            }
        }
        /* DOS Date and time:
            MS-DOS date. The date is a packed value with the following format. Bits Description
                0-4 Day of the month (1–31)
                5-8 Month (1 = January, 2 = February, and so on)
                9-15 Year offset from 1980 (add 1980 to get actual year)
            MS-DOS time. The time is a packed value with the following format. Bits Description
                0-4 Second divided by 2
                5-10 Minute (0–59)
                11-15 Hour (0–23 on a 24-hour clock)
        */
        private uint DateTimeToDosTime(DateTime _dt)
        {
            return (uint)(
                (_dt.Second / 2) | (_dt.Minute << 5) | (_dt.Hour << 11) |
                (_dt.Day<<16) | (_dt.Month << 21) | ((_dt.Year - 1980) << 25));
        }
        private DateTime DosTimeToDateTime(uint _dt)
        {
            return new DateTime(
                (int)(_dt >> 25) + 1980,
                (int)(_dt >> 21) & 15,
                (int)(_dt >> 16) & 31,
                (int)(_dt >> 11) & 31,
                (int)(_dt >> 5) & 63,
                (int)(_dt & 31) * 2);
        }

        /* CRC32 algorithm
          The 'magic number' for the CRC is 0xdebb20e3.
          The proper CRC pre and post conditioning
          is used, meaning that the CRC register is
          pre-conditioned with all ones (a starting value
          of 0xffffffff) and the value is post-conditioned by
          taking the one's complement of the CRC residual.
          If bit 3 of the general purpose flag is set, this
          field is set to zero in the local header and the correct
          value is put in the data descriptor and in the central
          directory.
        */
        private void UpdateCrcAndSizes(ref ZipFileEntry _zfe)
        {
            long lastPos = this.ZipFileStream.Position;  // remember position

            this.ZipFileStream.Position = _zfe.HeaderOffset + 8;
            this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);  // zipping method

            this.ZipFileStream.Position = _zfe.HeaderOffset + 14;
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.Crc32), 0, 4);  // Update CRC
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.CompressedSize), 0, 4);  // Compressed size
            this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.FileSize), 0, 4);  // Uncompressed size

            this.ZipFileStream.Position = lastPos;  // restore position
        }
        // Replaces backslashes with slashes to store in zip header
        private string NormalizedFilename(string _filename)
        {
            string filename = _filename.Replace('\\', '/');

            int pos = filename.IndexOf(':');
            if (pos >= 0)
                filename = filename.Remove(0, pos + 1);

            return filename; //.Trim('/');
        }
        // Reads the end-of-central-directory record
        private bool ReadFileInfo()
        {
            if (this.ZipFileStream.Length < 22)
                return false;

            try
            {
                this.ZipFileStream.Seek(-17, SeekOrigin.End);
                BinaryReader br = new BinaryReader(this.ZipFileStream);
                do
                {
                    this.ZipFileStream.Seek(-5, SeekOrigin.Current);
                    UInt32 sig = br.ReadUInt32();
                    if (sig == 0x06054b50)
                    {
                        this.ZipFileStream.Seek(6, SeekOrigin.Current);

                        UInt16 entries = br.ReadUInt16();
                        Int32 centralSize = br.ReadInt32();
                        UInt32 centralDirOffset = br.ReadUInt32();
                        UInt16 commentSize = br.ReadUInt16();

                        // check if comment field is the very last data in file
                        if (this.ZipFileStream.Position + commentSize != this.ZipFileStream.Length)
                            return false;

                        // Copy entire central directory to a memory buffer
                        this.ExistingFiles = entries;
                        this.CentralDirImage = new byte[centralSize];
                        this.ZipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        this.ZipFileStream.Read(this.CentralDirImage, 0, centralSize);

                        // Leave the pointer at the begining of central dir, to append new files
                        this.ZipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        return true;
                    }
                } while (this.ZipFileStream.Position > 0);
            }
            catch { }

            return false;
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Closes the Zip file stream
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
        #endregion
    }
//}

</script>
