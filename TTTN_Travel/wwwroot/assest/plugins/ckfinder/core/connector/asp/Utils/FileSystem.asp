<script runat="server" language="VBScript">
' CKFinder
' ========
' http://cksource.com/ckfinder
' Copyright (C) 2007-2015, CKSource - Frederico Knabben. All rights reserved.
'
' The software, this file and its contents are subject to the CKFinder
' License. Please read the license.txt file before using, installing, copying,
' modifying or distribute this file or part of its contents. The contents of
' this file is part of the Source Code of CKFinder.

''
' @package CKFinder
' @subpackage Utils
' @copyright CKSource - Frederico Knabben
'
class CKFinder_Connector_Utils_FileSystem

	'' Reference to Scripting.FileSystemObject
	 '
	Private oFSO

	' A file that might be used by Asp.net to verify that the call is from this script.
	Private tempFilePath

	Private Sub Class_Initialize()
		Set oFSO = server.CreateObject("Scripting.FileSystemObject")
		tempFilePath = ""
	End Sub

	Private Sub Class_Terminate()
		' If there was a temp file created, delete it.
		cleanUpTempFile

		Set oFSO = nothing
	End sub

	'' Combines two paths to generate a valid path, correctly appending the \ as needed
	 '
	 ' @static
	 ' @access public
	 ' @param string path1 first path
	 ' @param string path2 scecond path
	 ' @return string
	 '
	Public function combinePaths(path1, path2)
		Dim sPath1, sPath2
		sPath1 = path1 & ""
		sPath2 = Replace(path2 & "", "/", "\")
		combinePaths = oFSO.BuildPath(sPath1, sPath2)
	End Function

	'' Combines two URL to generate a valid path, correctly appending the / as needed
	 '
	 ' @static
	 ' @access public
	 ' @param string path1 first path
	 ' @param string path2 scecond path
	 ' @return string
	 '
	Public function combineURLs(path1, path2)
		Dim sPath1, sPath2
		sPath1 = path1 & ""
		sPath2 = path2 & ""
		If sPath1<>"" Then If (Right(sPath1, 1) = "/") Then sPath1 = Left(sPath1, Len(sPath1)-1)
		If sPath2<>"" Then If Left(sPath2, 1) = "/" Then sPath2 = right(sPath2, Len(sPath2)-1)
		combineURLs = sPath1 & "/" & sPath2
		If (combineURLs = "/") Then combineURLs = ""
	End Function


	Public Function FileExists(filename)
		If (filename = "") Then
			FileExists = False
			Exit function
		End If

		FileExists = oFSO.FileExists(filename)
	End Function

	Public Function FolderExists( foldername )
		If (foldername = "" Or foldername = "/") Then
			FolderExists = False
			Exit function
		End If
		If (Right(foldername, 1) = "\") Then foldername = Left(foldername, Len(foldername)-1)

		FolderExists = oFSO.FolderExists(foldername)
	End Function


	Public Function DeleteFile( filename )
		If (filename = "") Then
			DeleteFile = False
			Exit function
		End If

		If (FileExists(filename)) Then
			Dim eNumber, eDescription
			On Error Resume next
			oFSO.DeleteFile filename

			eNumber = Err.number
			eDescription = Err.description
			On Error goto 0

			If (eNumber<>0) Then
				Err.raise vbObjectError + CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Failed to Delete File", "(Error: " & eNumber & ", " & eDescription & ") File " & filename
				DeleteFile = False
				Exit function
			End if

			DeleteFile = true
		Else
			DeleteFile = false
		End if
	End Function

	Public Function DeleteFolder( foldername )
		If (foldername = "" Or foldername = "/") Then
			DeleteFolder = False
			Exit function
		End If
		If (Right(foldername, 1) = "\") Then foldername = Left(foldername, Len(foldername)-1)

		If (FolderExists(foldername)) then
			oFSO.DeleteFolder foldername
			DeleteFolder = true
		Else
			DeleteFolder = false
		End if
	End Function

	'' Get file extension (if lastPart is true, only last part is returned - e.g. extension of file.foo.bar.jpg = jpg)
	 '
	 ' @param string fileName
	 ' @return string
	 '
	Public Function GetExtension( filename, lastPart )
		If lastPart Then
			GetExtension = oFSO.GetExtensionName(filename)
		Else
			Dim DotPos
			DotPos = Instr(fileName, "." )
			If DotPos < Len(fileName) Then
				GetExtension = Mid(fileName, DotPos + 1)
			Else
				GetExtension = ""
			End If
		End If
	End Function

	Public Function GetFileName(filename)
		GetFileName = oFSO.GetFileName(filename)
	End Function

	Public function GetFileSize( filePath )
		Dim oFile
		Set oFile = oFSO.GetFile( filePath )
		GetFileSize = oFile.size
		Set oFile = Nothing
	End Function

	Public function GetParentFolderName(dir)
		GetParentFolderName = oFSO.GetParentFolderName(dir)
	End Function

	Public Function RenameFolder( oldFolderName, newFolderName )
		If (oldFolderName = "" Or oldFolderName = "/") Or (newFolderName = "" Or newFolderName = "/") Then
			RenameFolder = False
			Exit function
		End If
		If (Right(oldFolderName, 1) = "\") Then oldFolderName = Left(oldFolderName, Len(oldFolderName)-1)
		If (Right(newFolderName, 1) = "\") Then newFolderName = Left(newFolderName, Len(newFolderName)-1)

		If (FolderExists(oldFolderName) And Not(FolderExists(newFolderName)) ) then
			oFSO.MoveFolder oldFolderName, newFolderName
			RenameFolder = true
		Else
			RenameFolder = false
		End if
	End Function

	Public Function RenameFile( oldFileName, newFileName )
		If (oldFileName = "") Or (newFileName = "") Then
			RenameFile = False
			Exit function
		End If

		If (FileExists(oldFileName) And Not(FileExists(newFileName)) ) then
			oFSO.MoveFile oldFileName, newFileName
			RenameFile = true
		Else
			RenameFile = false
		End if
	End Function

	Public Function CopyFile( source, target )
		If (source = "") Or (target = "") Then
			RenameFile = False
			Exit function
		End If

		If (FileExists(source) And Not(FileExists(target)) ) then
			oFSO.CopyFile source, target
			CopyFile = true
		Else
			CopyFile = false
		End if
	End Function

	'' Check whether fileName is a valid file name, return true on success
	 '
	 ' @static
	 ' @access public
	 ' @param string fileName
	 ' @return boolean
	 '
	Public function checkFileName(fileName)

		if (isempty(fileName) Or (fileName="")) then
			checkFileName = False
			Exit function
		End if

		if (right(fileName, Len(fileName)-1)="." or inStr(fileName, "..")) then
			checkFileName = False
			Exit function
		End if

		' Check \ / | : ? * " < >
		Dim pattern
		If oCKFinder_Factory.Config.getDisallowUnsafeCharacters Then
			' Include  ;
			pattern = "(\\|\/|\||:|\?|\*|""|\<|\>|;|[\u0000-\u001F]|\u007F)"
		Else
			pattern = "(\\|\/|\||:|\?|\*|""|\<|\>|[\u0000-\u001F]|\u007F)"
		End if
		checkFileName = Not oCKFinder_Factory.RegExp.MatchesPattern( pattern, fileName )
	End function

	'' Check whether folderName is a valid folder name, return true on success
	 '
	 ' @static
	 ' @access public
	 ' @param string folderName
	 ' @return boolean
	 '
	Public function checkFolderName(folderName)

		if (Not checkFileName( folderName )) then
			checkFolderName = False
			Exit function
		End if

		If oCKFinder_Factory.Config.getDisallowUnsafeCharacters Then
			' Check . and ;
			checkFolderName = Not oCKFinder_Factory.RegExp.MatchesPattern( "(\.)", folderName )
		Else
			checkFolderName = true
		End If
	End function

 	'' Check whether path contains valid folders names, return true on success
	 '
	 ' @static
	 ' @access public
	 ' @param string path
	 ' @return boolean
	 '
	Public function checkFolderPath(path)
		' path isn't supposed to include the drive root or the call to CheckFolderName will fail while checking C:
		Dim paths, i
		If (path="") Then
			checkFolderPath = true
			Exit function
		End if

		paths = Split(path, "\")
		For i=0 To UBound(paths)
			Dim partial
			partial = paths(i)
			If (partial<>"") Then
				If Not checkFolderName(partial) Then
					checkFolderPath = false
					Exit function
				End if
			End if
		next

		checkFolderPath = true
    End function

	'' Returns a Files object with the files contained on the provided folder.
	 ' It doesn't perform any filtering on the results
	Public Function GetFiles( sFolderPath )
		Dim oCurrentFolder
		Set oCurrentFolder = oFSO.GetFolder( sFolderPath )
		Set GetFiles = oCurrentFolder.Files
	End function

	'' Returns a Dictionary with the folders contained on the provided folder.
	 ' It sorts the folders, but doesn't perform any filtering on the results
	Public Function GetSubFolders( sFolderPath )
		Dim oCurrentFolder, oFolders, oFolder
		Set oCurrentFolder = oFSO.GetFolder( sFolderPath )
		Set oFolders = oCurrentFolder.SubFolders

		' #949 Sort the folders just in case...
		Dim oSortedFolders
		Set oSortedFolders = Server.CreateObject("Scripting.Dictionary")

		Dim index, j, folderCount, buffer
		folderCount = oFolders.count

		index = 1
		For Each oFolder in oFolders
			oSortedFolders.Add index, oFolder
			index = index + 1
		Next

		for index = 1 to folderCount
			for j = (index + 1) to folderCount
				if strComp(oSortedFolders(index).name, oSortedFolders(j).name, 0) = 1 then
					Set buffer = oSortedFolders(index)
					Set oSortedFolders(index) = oSortedFolders(j)
					Set oSortedFolders(j) = buffer
				end if
			next
		next

		Set GetSubFolders = oSortedFolders
	End Function

	'' Returns true if directory has subfolders, taking into account the ACL
	 '
	 ' @param string clientPath client path (with trailing slash)
	 ' @param object resourceType resource type configuration
	 ' @return boolean
	 '
	public function hasChildren( clientPath, resourceType )
		Dim sServerDir
		sServerDir = combinePaths( resourceType.getDirectory(), clientPath )

		if (Not oFSO.FolderExists(sServerDir)) Then
			hasChildren = False
			Exit function
		End If

		Dim oFolder, i, acl, aclMask, oFolders
		hasChildren = false
		Set oFolder = oFSO.GetFolder( sServerDir )
		Set oFolders = oFolder.SubFolders

		Set acl = oCKFinder_Factory.Config.getAccessControlConfig()
		For Each oFolder in oFolders
			Dim subDirName
			subDirName = oFolder.name

 			aclMask = acl.getComputedMask(resourceType.getName(), clientPath & subDirName)

			if ( (aclMask and CKFINDER_CONNECTOR_ACL_FOLDER_VIEW) = CKFINDER_CONNECTOR_ACL_FOLDER_VIEW ) then
 				if not(resourceType.checkIsHiddenFolder(subDirName)) then
 					hasChildren = True
					Exit for
				End If
			End If
		Next

		Set oFolder = Nothing
	End Function

    '' Check if given directory is empty (files or subfolders). No check on ACL or other features performed
     '
     ' @param string $dirname
     ' @access public
     ' @static
     ' @return bool
	public function isEmptyDir( sServerDir )
		if (Not oFSO.FolderExists(sServerDir)) Then
			isEmptyDir = False
			Exit function
		End If

		Dim oFolder
		Set oFolder = oFSO.GetFolder( sServerDir )
		isEmptyDir = (oFolder.SubFolders.count = 0 And oFolder.Files.count = 0)
	End function


	'' Return file name without extension (without dot & last part after dot)
	 '
	 ' @param string fileName
	 ' @return string
	 '
	function getFileNameWithoutExtension( fileName, withoutLastPart )
		if (withoutLastPart) Then
			getFileNameWithoutExtension = oFSO.getBaseName(fileName)
		Else
			Dim DotPos
			DotPos = Instr(fileName, "." )
			If DotPos < Len(fileName) Then
				getFileNameWithoutExtension = Mid(fileName, 1, DotPos-1)
			Else
				getFileNameWithoutExtension = fileName
			End If
		End If
	End function


	'' Create directory recursively
	 '
	 ' @static
	 ' @param string dir
	 ' @param int mode
	 ' @return boolean
	 '
	function createDirectoryRecursively(path)
		Dim dir
		dir = path
		If (Right(dir, 1)="\") Then dir = Left(dir, Len(dir)-1)

		' Check for empty path. This shouldn't happen
		If (dir = "") Then
			createDirectoryRecursively = false
			Exit function
		End If

		If oFSO.FolderExists(dir) Then
			createDirectoryRecursively = true
			Exit function
		End If

		Dim parentFolder, folderName
		parentFolder = oFSO.GetParentFolderName(dir)
		If Not createDirectoryRecursively( parentFolder ) Then
			createDirectoryRecursively = false
			Exit function
		End If

		' Perform additional security check
		folderName = Right( dir, Len(dir) - (Len(parentFolder) + 1) )
		If Not checkFolderName( folderName ) Then
			createDirectoryRecursively = false
			Exit function
		End If

		Dim eNumber, eDescription
		On Error Resume Next
		oFSO.CreateFolder(dir)

		eNumber = Err.number
		eDescription = Err.description
		On Error goto 0

		If (eNumber<>0) Then
			Err.raise vbObjectError + CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Failed to create directory", "(Error: " & eNumber & ", " & eDescription & ") Folder: " & dir
		End if

		createDirectoryRecursively = true
	End Function


    '' Autorename file if previous name is already taken
     '
     ' @param string $filePath
     ' @param string $fileName
    public function autoRename( filePath, fileName )
		fileName = Replace(fileName, "/", "\")
		If Not(FileExists( combinePaths( filePath, fileName) ) ) Then
			autoRename = fileName
			Exit function
		End If

		Dim iCounter, baseName, extension, tempFileName, destinationFilePath, folder
		iCounter = 1
		folder = GetParentFolderName(fileName)
		baseName = getFileNameWithoutExtension(fileName, false)
		baseName = combinePaths(folder, baseName)
		extension = getExtension(fileName, false)
		do
			tempFileName = baseName & "(" & iCounter & ")." & extension

			destinationFilePath = combinePaths( filePath , tempFileName)
			if (Not FileExists(destinationFilePath)) then
				autoRename = tempFileName
				Exit function
			End if
			iCounter = iCounter +1
		Loop While true
    End function

	'' Send the contents of a file back to the client.
	 ' If filename is specified then it's sent as an attachment
	Public sub sendFile(filePath, contentType, fileName)
		Dim oStream, oFile, FileSize, FileDate, FileSent
		Const BlockSize = 100000

		Response.Buffer = true
		Response.clear

		Set oFile = oFSO.GetFile( filePath )
		FileSize = oFile.size
		FileDate = oFile.DateLastModified
		Set oFile = Nothing

		Dim sLastModified, strIfModifiedSince
		Dim Etag, sentEtag

		' a simple etag based on file date + time and filesize
		Etag = """" & Hex(FileDate) & Hex( (FileDate-Fix(FileDate)) * 60*60*24) &  ":" & Hex(FileSize) & """"
		sLastModified = FormatDateRFC822(FileDate)

		sentEtag = Request.ServerVariables("HTTP_IF_NONE_MATCH")
		strIfModifiedSince=Request.ServerVariables("HTTP_IF_MODIFIED_SINCE")
		If (Len(sentEtag)) Then
			If (sentEtag=Etag) And (strIfModifiedSince=sLastModified) Then
				Response.Status = "304 Not Modified"
				Response.End
			End If
		Else
			' If it hasn't sent the etag, then check only the date.
			If (Len(strIfModifiedSince)) Then
				If CompareRFC822Dates(strIfModifiedSince, sLastModified) Then
					Response.Status = "304 Not Modified"
					Response.End
				End If
			End If
		End If

		' Avoids cache:
		'		Response.Expires = 0
		' Allows public cache systems to cache the response
		'		response.CacheControl="Public"
		Response.ContentType = contentType
		Response.AddHeader "Last-Modified", sLastModified
		Response.AddHeader "Etag", Etag
		If (fileName<>"") Then
			Dim encodedName, browser
			encodedName = Replace(fileName, """", "\""")

			' Encode filename for IE
			browser = Request.ServerVariables("HTTP_USER_AGENT")
			if (InStr(browser, "MSIE ")) then
				encodedName = Replace(Replace(Server.UrlEncode(encodedName), "+", " "), "%2E", ".")
			End If

			Response.AddHeader "content-disposition", "attachment; filename=""" & encodedName & """"
		End if
		Response.AddHeader "Content-Length", FileSize
		Response.Flush

		If (FileSize>0) then
			Set oStream = CreateObject("ADODB.Stream")

			With oStream
				.Open
				.Type = 1
				.LoadFromFile filePath
			End With

			' Send data
			FileSent = 0
			While FileSent + BlockSize < FileSize
				Response.BinaryWrite oStream.Read(BlockSize)
				FileSent = FileSent + BlockSize
				Response.Flush
			Wend
			Response.BinaryWrite oStream.Read(FileSize - FileSent)

			oStream.close
			Set oStream = Nothing
		End if
'		Set oFSO = nothing
	end sub

	'' Returns the tmp path as string
	Public Function getTmpDir()
		' If the user has set a path in config.asp use it, else use the system temp path
		If (CKFinderTempPath<>"") Then
			If Not( createDirectoryRecursively( CKFinderTempPath ) ) Then
				Exit function
			End if

			getTmpDir = CKFinderTempPath
		else
			getTmpDir = oFSO.GetSpecialFolder(2).path
		End If
	End Function

	'' Creates a temporay file for validation from Asp.net
	 ' Returns the name of the file, excluding the path and the special extension:
	 '	.ckfindertemp
	 '
	Function createTempFile()

		Dim tfolder, tname
		' If the user has set a path in config.asp use it, else use the system temp path
		If (CKFinderTempPath<>"") Then
			If Not( createDirectoryRecursively( CKFinderTempPath ) ) Then
				Exit function
			End if

			Set tfolder = oFSO.GetFolder( CKFinderTempPath )
		else
			Set tfolder = oFSO.GetSpecialFolder(2)
		End If

		' If it has been called previously, delete that file
		cleanUpTempFile()

		tname = oFSO.GetTempName
		' Store the path of the file so we can always delete it, not relying on the asp.net code
		tempFilePath = combinePaths( tfolder.path, tname & ".ckfindertemp")

		CreateTextFile tempFilePath, empty
		Set tfolder = Nothing

		createTempFile = tname
	End Function

	''
	' Creates a text file, with some contents (or empty)
	'
	Function CreateTextFile( sFilePath, sContents )
		Dim fileStream
		Set fileStream = oFSO.CreateTextFile( sFilePath )
		If Not( IsEmpty(sContents) ) Then
			fileStream.Write sContents
		End If
		fileStream.close

		CreateTextFile = true
	End Function

	''
	' Deletes the temp File that was created
	'
	Private Sub cleanUpTempFile()
		' If there's no file exit
		If tempFilePath="" Then Exit Sub

		if (oFSO is nothing) then	Set oFSO = server.CreateObject("Scripting.FileSystemObject")

		On Error Resume next
		' Call the delete routine
		' In some scenarios it might be possible to create the file but not to delete it
		DeleteFile tempFilePath
		on error goto 0

		' Reset the variable
		tempFilePath = ""
	End Sub

	''
	' Creates a text file, with some contents in UTF8
	'
	Function CreateTextFileUTF8( sFilePath, sContents )
		Dim oStream
		Set oStream = Server.CreateObject("ADODB.Stream")
		oStream.Open
		oStream.CharSet = "UTF-8"
		oStream.WriteText sContents
		oStream.SaveToFile sFilePath, 2 ' UTF-8
		oStream.Close

		CreateTextFileUTF8 = true
	End Function

	''
	' Reads the contents of a text file (UTF-8)
	'
	Function ReadTextFile( sFilePath )
		Dim oStream
		Set oStream = Server.CreateObject("ADODB.Stream")
		oStream.Open
		oStream.LoadFromFile sFilePath
		oStream.CharSet = "UTF-8"
		ReadTextFile = oStream.ReadText()
		oStream.Close
	End Function

	''
	' Reads the first bytes in the  contents of a text file (UTF-8)
	'
	Function ReadPartialTextFile( sFilePath, length )
		Dim oStream
		Set oStream = Server.CreateObject("ADODB.Stream")
		oStream.Open
		oStream.LoadFromFile sFilePath
		oStream.CharSet = "UTF-8"
		ReadPartialTextFile = oStream.ReadText(length)
		oStream.Close
	End Function


	'''
	' Verifies whether a file contains HTML unless it's one of the allowed formats
	' returns true if it isn't specified as a valid container but includes HTML
	' false : everything is OK
	Public Function detectHtml( filePath )
		Dim sExtension, data
		sExtension = getExtension( filePath, true )

		If IsHtmlExtension( sExtension ) Then
			detectHtml = false
			Exit Function
		End If

		data = ReadPartialTextFile( filePath, 1024 )

		detectHtml = SniffHtml( data )
	End Function

	Private Function IsHtmlExtension( sExt )
		Dim sHtmlExtensions
		sHtmlExtensions = oCKFinder_Factory.Config.getHtmlExtensions()
		If sHtmlExtensions = "" Then
			Exit Function
		End If

		Dim oRE
		Set oRE = New RegExp
		oRE.IgnoreCase	= True
		oRE.Global		= True
		oRE.Pattern		= sHtmlExtensions

		IsHtmlExtension = oRE.Test(sExt)

		Set oRE	= Nothing
	End Function

	Private Function SniffHtml( sData )

		Dim oRE
		Set oRE = New RegExp
		oRE.IgnoreCase	= True
		oRE.Global		= True

		Dim aPatterns
		aPatterns = Array( "<!DOCTYPE\W*X?HTML", "<(body|head|html|img|pre|script|table|title)", "type\s*=\s*[\'""]?\s*(?:\w*/)?(?:ecma|java)", "(?:href|src|data)\s*=\s*[\'""]?\s*(?:ecma|java)script:", "url\s*\(\s*[\'""]?\s*(?:ecma|java)script:" )

		Dim i
		For i = 0 to UBound( aPatterns )
			oRE.Pattern = aPatterns( i )
			If oRE.Test( sData ) Then
				SniffHtml = True
				Exit Function
			End If
		Next

		SniffHtml = False
	End Function

End Class

</script>
