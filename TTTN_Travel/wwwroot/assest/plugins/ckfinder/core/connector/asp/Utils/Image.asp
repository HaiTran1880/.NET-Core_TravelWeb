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

	''
	' @package CKFinder
	' @subpackage Utils
	' @copyright CKSource - Frederico Knabben
	'
class CKFinder_Connector_Utils_Image
	' Name of the component that should be used for image manipulation
	Private sComponentName
	Private oImageHandler

	Private Sub Class_Initialize()

	End Sub

	Private Sub Class_Terminate()
		Set oImageHandler = Nothing
	End sub

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	''
	' Detects which component is available to handle image manipulation
	'
	' @return String
	'
	private Function DetectComponent()
		Dim component, expireDate

		Set component = new CKFinder_Utils_ImageHandler_AspNet
		If component.Enabled() then
			DetectComponent = "Asp.Net"
			Exit function
		End If

		Set component = new CKFinder_Utils_ImageHandler_Persits
		If component.Enabled() then
			DetectComponent = "Persits.Jpeg"
			Exit function
		End If

		Set component = new CKFinder_Utils_ImageHandler_Briz
		If component.Enabled() then
			DetectComponent = "briz.AspThumb"
			Exit Function
		End If

		Set component = new CKFinder_Utils_ImageHandler_AspImage
		If component.Enabled() then
			DetectComponent = "AspImage.Image"
			Exit function
		End If

		Set component = new CKFinder_Utils_ImageHandler_ShotGraph
		If component.Enabled() then
			DetectComponent = "shotgraph.image"
			Exit Function
		End If

		throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to find an image manipulation component", ""
	End function

	' Find out which component should be used
	Public Property Get ComponentName()
		If (IsEmpty(sComponentName)) then
			sComponentName = oCKFinder_Factory.Config.getImagesConfig.Component
			If sComponentName = "" Or LCase(sComponentName)="none" Then sComponentName = "None"

			If (sComponentName = "Auto") Then
				sComponentName = DetectComponent()
				oCKFinder_Factory.Config.getImagesConfig.Component = sComponentName
			End If
		End If
		ComponentName = sComponentName
	End Property

	Private Property Get ImageHandler()
		If IsEmpty(oImageHandler) then
			Select Case ComponentName
				Case "Asp.Net"
					Set oImageHandler = new CKFinder_Utils_ImageHandler_AspNet

				Case "Persits.Jpeg"
					Set oImageHandler = new CKFinder_Utils_ImageHandler_Persits

				Case "briz.AspThumb"
					Set oImageHandler = new CKFinder_Utils_ImageHandler_Briz

				' Doesn't deal properly with gifs and the current demo is expired, so it won't work in normal servers.
				' It also tends to generate crashes very easily.
				Case "AspImage.Image"
					Set oImageHandler = new CKFinder_Utils_ImageHandler_AspImage

				Case "shotgraph.image"
					Set oImageHandler = new CKFinder_Utils_ImageHandler_ShotGraph

				Case else
					throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Invalid image manipulation component", ComponentName
			End select
		End If
		Set ImageHandler = oImageHandler
	End Property


	''
	' Checks that a file is an image
	' It only checks the extension
	' @return Boolean
	'
	Public Function isImage( filePath )
		Dim extension
		extension = LCase( oCKFinder_Factory.UtilsFileSystem.GetExtension( filePath, true ) )
		If (extension="jpg" Or extension="jpeg" Or extension="bmp" Or extension="png" Or extension="gif") then
			isImage = True
		Else
			isImage = false
		End if
	End Function

	''
	' Checks that a file is really a valid image
	' @return Boolean
	'
	Public Function isImageValid( filePath )
		isImageValid = False
		If Not(isImage( filePath ) ) Then
			Exit function
		End If

		' If there is no component selected, then everything is valid.
		If (ComponentName="None") Then
			isImageValid = True
		else
			' It will try to open the file, if it works, we assume that it was a good file.
			isImageValid = ImageHandler.validateImage(filePath)
		End If
	End Function

	' Just for logging and find old versions.
	Public Function version()
		version = ImageHandler.version()
	End Function

	Public Function getImageSize( filePath )
		Set getImageSize = ImageHandler.getImageSize( filePath )
	End Function

	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		If Not( isImage( sourceFile ) ) Then Exit Function

		createThumb = ImageHandler.createThumb(sourceFile, targetFile, CInt(maxWidth), CInt(maxHeight), CInt(quality), preserveAspectRatio)
	End function

	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		If Not( isImage( sourceFile ) ) Then Exit Function

		createWatermark = ImageHandler.createWatermark(sourceFile, watermarkFile, CInt(marginLeft), CInt(marginBottom), CInt(quality), CInt(transparency))
	End function


	''
	' Return aspect ratio size, returns class CKFinder_Size
	' <pre>
	'	[Width] => 80
	'	[Heigth] => 120
	' </pre>
	'
	' @param int maxWidth
	' @param int maxHeight
	' @param int actualWidth
	' @param int actualHeight
	' @return CKFinder_Size
	'
	public Function GetAspectRatioSize( maxWidth, maxHeight, actualWidth, actualHeight )
		' Creates the Size object to be returned
		dim oSize, iFactorX, iFactorY

		Set oSize = new CKFinder_Size
		oSize.Width = maxWidth
		oSize.Height = maxHeight

		' Calculates the X and Y resize factors
		iFactorX = CSng(maxWidth) / cSng(actualWidth)
		iFactorY = CSng(maxHeight) / CSng(actualHeight)

		' If some dimension have to be scaled
		if ( iFactorX <> 1 or iFactorY <> 1 ) then
			' Uses the lower Factor to scale the oposite size
			if ( iFactorX < iFactorY ) then
				oSize.Height = cInt( CSng(actualHeight) * iFactorX )
			else
				if ( iFactorX > iFactorY ) then oSize.Width = cInt( cSng(actualWidth) * iFactorY )
			End if
		End if

		if ( oSize.Height <= 0 ) Then oSize.Height = 1
		if ( oSize.Width <= 0 ) Then oSize.Width = 1

		' Returns the Size
		Set GetAspectRatioSize = oSize
	End Function

End Class ' Main Image class

' Auxiliary class
Class CKFinder_Size
	Public Width
	Public Height
End Class

' .Net Implementation
Class CKFinder_Utils_ImageHandler_AspNet

	' Xml node returned by the call to Asp.Net
	Private responseNode

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	Public function Enabled()
		enabled = false

		On Error Resume next

		LoadUrl AspNetUrl() & "&command=IsEnabled"
		If ( Err.Number=0) Then
			Enabled= true
		End If
		On Error GoTo 0
	End Function

	Public Function Version()
		' Already reported in the wizard tests
		Version = ""
	End Function

	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		createThumb = CallAspNet( "&command=CreateThumbnail" & _
					"&InputImage=" & server.urlEncode(sourceFile) & _
					"&OutputThumbnail=" & server.urlEncode(targetFile) & _
					"&maxWidth=" & maxWidth & _
					"&maxHeight=" & maxHeight & _
					"&quality=" & quality )
	End function

	Public function ValidateImage(sourceFile)
		ValidateImage = CallAspNet( "&command=ValidateImage" & _
					"&InputImage=" & server.urlEncode(sourceFile) )
	End function


	Public function getImageSize(sourceFile)
		CallAspNet( "&command=GetImageSize" & _
					"&InputImage=" & server.urlEncode(sourceFile) )

		Dim oSize
		Set oSize = new CKFinder_Size

		oSize.Width =   CInt(responseNode.SelectSingleNode( "@Width" ).value)
		oSize.Height =  CInt(responseNode.SelectSingleNode( "@Height" ).value)

		Set getImageSize = oSize
	End Function

	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		createWatermark = CallAspNet( "&command=CreateWatermark" & _
					"&InputImage=" & server.urlEncode(sourceFile) & _
					"&watermarkFile=" & server.urlEncode(watermarkFile) & _
					"&marginLeft=" & marginLeft & _
					"&marginBottom=" & marginBottom & _
					"&transparency=" & transparency & _
					"&quality=" & quality )
	End function

	Private Function CallAspNet( parameters )
		Dim url, errNumber, errDescription
		url = AspNetUrl() & parameters

		On Error Resume next
		If not LoadUrl( url ) Then
			errNumber = Err.Number
			errDescription = Err.Description
			On Error goto 0

			If errNumber<>0 Then
				If (errNumber = vbObjectError + CKFINDER_CONNECTOR_ERROR_UPLOADED_INVALID) Then
					CallAspNet = False
					Exit function
				End If

				throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Error returned in call to Asp.Net", "Error returned in call to " & url & ". (" & (errNumber - vbObjectError) & ", " & errDescription & ")"
			End If
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Failed to call Asp.Net", "Failed to call url " & url
		End If
		On Error Goto 0

		CallAspNet = true
	End Function

	''
	' Builds the full url to the asp.net loopback page
	' it should be placed in the same folder than the connector.asp
	'
	Private Property Get AspNetUrl()
		Dim url
		url = ""
		If (UCase(Request.ServerVariables("HTTPS")) = "ON") Then
			url = "https://"
		Else
			url = "http://"
		End if
		url = url & Request.ServerVariables("SERVER_NAME")
		Dim port
		port = request.ServerVariables("SERVER_PORT")
		If (port<>"80") Then url = url & ":" & port
		url = url & oCKFinder_Factory.RegExp.ReplacePattern( "/connector.asp$", Request.ServerVariables("URL"), "/")
		url = url & "loopback.aspx"

		' Generate a temporary file for "authentication".
		url = url & "?tmp=" & oCKFinder_Factory.UtilsFileSystem.createTempFile()

		AspNetUrl = url
	End Property

	''
	' Calls an url
	' The url must return a XML response or an error will be raised.
	' returns true if the call was OK (0 in value of Connector/Error/@number)
	'
	Private Function LoadUrl( sUrlToCall )
		Dim oXmlHttp, authentication
		Dim node, value

		Set oXmlHttp = Server.CreateObject("Msxml2.ServerXMLHTTP.3.0")

		authentication = Request.ServerVariables("AUTH_TYPE")
		If authentication="Basic" Then
			' Basic authentication
			oXmlHttp.Open "GET", sUrlToCall, False, Request.ServerVariables("AUTH_USER") & "", Request.ServerVariables("AUTH_PASSWORD") & ""
		Else
			' No authentication "" or Windows authentication "NTLM"
			' For Windows authentication proxycfg must have been configured properly
			' http://docs.cksource.com/CKFinder_2.x/Developers_Guide/ASP/Troubleshooting/Windows_Authentication
			oXmlHttp.Open "GET", sUrlToCall, False
		End If

		oXmlHttp.Send

		if ( (oXmlHttp.status = 200 or oXmlHttp.status = 304) and Not(IsNull(oXmlHttp.responseXML) ) And Not(IsNull( oXmlHttp.responseXML.firstChild)) ) then
		'	this.DOMDocument = oXmlHttp.responseXML ;
		Else
			Err.Raise vbObjectError + CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to LoadUrl (" & sUrlToCall & ")", oXmlHttp.responseText
		End if

		On Error Resume next
		Set node = oXmlHttp.responseXML.SelectSingleNode( "Connector/Error/@number" )
		On Error goto 0
		If (node Is Nothing) Then
			Err.Raise vbObjectError + CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Invalid XML response", oXmlHttp.responseText
		End if

		value = CInt(node.value)
		If (value<>0) Then
			Err.Raise vbObjectError + value, "Error returned in LoadUrl",  oXmlHttp.responseText
		End If

		' If the response includes a "Response" node, store it to parse it outside this function.
		Set responseNode = oXmlHttp.responseXML.SelectSingleNode( "Connector/Response" )

		Set oXmlHttp = Nothing

		LoadUrl = true
	End Function

End Class


' Persits Implementation
Class CKFinder_Utils_ImageHandler_Persits

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	'' returns a long with the version number for easy check of features
	private Function versionNumber(obj)
		Dim data
		' Returns the current version of the component in the format "1.6.0.0"
		data = obj.version
		' Convert it to a Long
		data = Replace(data, ".", "")
		data = Replace(data, "(64-bit)", "")
		versionNumber = CLng(trim(data))
	End Function

	Public Function Enabled()
		Dim component, expireDate
		On Error resume next
		Enabled = false
		Set component = Server.CreateObject("Persits.Jpeg")
		If (Err.number=0) Then
			' Check now that it isn't an expired version
			expireDate = component.expires

			Set component = Nothing
			If (expireDate = #9/9/9999# Or expireDate>Now()) then
				Enabled = true
			End if
		End If
		On Error goto 0
	End Function

	Public Function Version()
		Dim component
		On Error resume next
		Set component = Server.CreateObject("Persits.Jpeg")
		If (Err.number=0) Then
			Version = component.version

			Set component = Nothing
		End If
		On Error goto 0
	End Function

	Private isGif
	Private isPng

	Private Function openImage(sourceFile, checkExtension)
		Dim Jpeg
		' Create instance of AspJpeg
		On Error Resume next
		Set Jpeg = Server.CreateObject("Persits.Jpeg")

		If (Err.number<>0) Then
			Set Jpeg = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to create Persits.Jpeg component.", Err.description
			Exit function
		End if

		If checkExtension Then
			Dim extension, theVersion
			theVersion = versionNumber(Jpeg)
			extension = LCase(Right(sourceFile, 4))
			If (extension = ".gif") And (theVersion >= 2000) Then isGif = true
			If (extension = ".png") And (theVersion >= 2100) Then isPng = true
		End If

		' Open source image
		If (checkExtension And isGif) Then
			Jpeg.Gif.Open sourceFile
		else
			Jpeg.Open sourceFile
		End If

		If (Err.number<>0) Then
			On Error goto 0
			Set Jpeg = nothing
			Set openImage = Nothing
			Exit function
		End if
		On Error goto 0

		Set openImage = Jpeg
	End function

	' Common final steps before saving
	Private Sub saveFile(Jpeg, targetFile)
		' Avoid problems with CMYK
		If (Not(isGif) And Not(isPng)) Then
			Jpeg.ToRGB
		End if

		If (isPng) Then
			' Output as PNG
			Jpeg.PNGOutput = True
		End If

		' Save to disk
		If (isGif) Then
			' Save the gif
			Jpeg.gif.save targetFile
		else
			Jpeg.Save targetFile
		End If
	End Sub

	Public function getImageSize(sourceFile)
		Dim Jpeg
		' reset private variables
		isGif = false
		isPng = false

		' Create instance of AspJpeg
		Set Jpeg = openImage(sourceFile, true)

		Set getImageSize = getSize(Jpeg)

		Set Jpeg = nothing
	End Function

	Private Function getSize(jpeg)
		Dim oSize
		Set oSize = new CKFinder_Size

		If (Jpeg Is Nothing) Then
			Set getSize = oSize
			Exit function
		End if

		If (isGif) Then
			oSize.Width =  Jpeg.Gif.Width
			oSize.Height =  Jpeg.Gif.Height
		else
			oSize.Width =  Jpeg.Width
			oSize.Height =  Jpeg.Height
		End If

		Set getSize = oSize
	End Function

	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		Dim Jpeg, oSize
		' reset private variables
		isGif = false
		isPng = false
		' Create instance of AspJpeg
		Set Jpeg = openImage(sourceFile, true)

		If (Jpeg Is Nothing) Then
			createThumb = false
			Exit function
		End if

		Set oSize = getSize(Jpeg)

		dim iFinalWidth, iFinalHeight
		' If 0 is passed in any of the max sizes it means that that size must be ignored,
		' so the original image size is used.
		iFinalWidth = maxWidth
		If (iFinalWidth = 0) Then iFinalWidth = oSize.Width
		iFinalHeight = maxHeight
		If (iFinalHeight = 0) Then iFinalHeight = oSize.Height

		if ( oSize.Width <= iFinalWidth and oSize.Height <= iFinalHeight ) then
			Set Jpeg = Nothing
			createThumb = oCKFinder_Factory.UtilsFileSystem.CopyFile( sourceFile, targetFile )
			Exit function
		End If

		dim oNewSize
		if ( preserveAspectRatio ) then
			' Gets the best size for aspect ratio resampling
			Set oNewSize = oCKFinder_Factory.UtilsImage.GetAspectRatioSize( iFinalWidth, iFinalHeight, oSize.Width, oSize.Height )
		else
			Set oNewSize = new CKFinder_Size
			oNewSize.Width = iFinalWidth
			oNewSize.Height = iFinalHeight
		End if

		' Resize
		If (isGif) Then
			Jpeg.Gif.Resize oNewSize.Width, oNewSize.Height
		else
			Jpeg.Width = oNewSize.Width
			Jpeg.Height = oNewSize.Height

			Jpeg.Quality = quality
		End If

		' create thumbnail and save it to disk
		saveFile Jpeg, targetFile

		Set Jpeg = Nothing

		createThumb = true
	End function


	Public function ValidateImage(sourceFile)
		Dim Jpeg
		' reset private variables
		isGif = false
		isPng = false
		' Create instance of AspJpeg
		Set Jpeg = openImage(sourceFile, true)

		If (Jpeg Is Nothing) Then
			ValidateImage = False
			Exit function
		End if

		ValidateImage = True
		Set Jpeg = nothing
	End Function

	'
	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		Dim Jpeg, Logo
		' reset private variables
		isGif = false
		isPng = false

		' Create instance of AspJpeg
		' AspJpeg limitation: Even if it's a gif, we need to load it in the main Jpeg object as the .Gif object doesn't have the .Canvas to draw the mask
		' animated gifs will be converted to a single frame to draw the watermark
		Set Jpeg = openImage(sourceFile, false)

		If (Jpeg Is Nothing) Then
			createWatermark = False
			Exit function
		End if

		Set Logo = openImage(watermarkFile, false)

		If (Logo Is Nothing) Then
			Set Jpeg = nothing
			createWatermark = False
			Exit function
		End If

		Dim marginTop, opacity, theVersion, extension
		marginTop = Jpeg.Height - (marginBottom + Logo.Height)

		theVersion = versionNumber(Jpeg)
		extension = LCase(Right(sourceFile, 4))
		If (extension = ".gif") And (theVersion >= 2000) Then isGif = true

		If theVersion<2100 Then
			opacity = 1 - transparency/100
			Jpeg.Canvas.DrawImage marginLeft, MarginTop, Logo, opacity
		else
			Jpeg.Canvas.DrawPng marginLeft, MarginTop, watermarkFile
		End if
		Set Logo = Nothing

		If (isGif) Then
			' Copy the image from the main object to the .Gif part so it's saved as gif
			Jpeg.Gif.AddImage Jpeg, 0, 0
		else
			Jpeg.Quality = quality
		End if

		' create thumbnail and save it to disk
		saveFile Jpeg, sourceFile

		Set Jpeg = Nothing

		createWatermark = true
	End function
End Class

' Briz.AspThumb Implementation
Class CKFinder_Utils_ImageHandler_Briz

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	Public Function Enabled()
		Dim component
		On Error resume next
		Enabled = false
		Set component = Server.CreateObject("briz.AspThumb")
		If (Err.number=0) Then
			Set component = Nothing
			enabled = true
		End If

		On Error goto 0
	End Function

	Public Function Version()
		' Not provided as of 2.0
		Version = ""
	End function


	Private Function openImage(sourceFile)
		Dim tn
		' Create instance of AspThumb
		On Error Resume next
		Set tn = Server.CreateObject("briz.AspThumb")

		If (Err.number<>0) Then
			Set tn = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to create briz.AspThumb component.", Err.description
			Exit function
		End if

		' Open source image
		tn.Load sourceFile

		If (Err.number<>0) Then
			tn.close
			Set tn = nothing
			Set openImage = nothing
			Exit function
		End if
		On Error goto 0

		Set openImage = tn
	End Function

	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		Dim tn
		' Create instance of AspThumb
		Set tn = openImage(sourceFile)

		If (tn Is Nothing) Then
			ValidateImage = False
			Exit function
		End if

		dim iFinalWidth, iFinalHeight
		' If 0 is passed in any of the max sizes it means that that size must be ignored,
		' so the original image size is used.
		iFinalWidth = maxWidth
		If (iFinalWidth = 0) Then iFinalWidth = tn.Width
		iFinalHeight = maxHeight
		If (iFinalHeight = 0) Then iFinalHeight = tn.Height

		if ( tn.Width <= iFinalWidth and tn.Height <= iFinalHeight ) Then
			tn.close
			Set tn = Nothing
			createThumb = oCKFinder_Factory.UtilsFileSystem.CopyFile( sourceFile, targetFile )
			Exit function
		End If

		dim oSize
		if ( preserveAspectRatio ) then
			' Gets the best size for aspect ratio resampling
			Set oSize = oCKFinder_Factory.UtilsImage.GetAspectRatioSize( iFinalWidth, iFinalHeight, tn.Width, tn.Height )
		else
			Set oSize = new CKFinder_Size
			oSize.Width = iFinalWidth
			oSize.Height = iFinalHeight
		End if

		tn.ResizeQuality = quality
		' Resize
		tn.Resize oSize.Width, oSize.Height

		' create thumbnail and save it to disk
		tn.Save targetFile
		tn.close

		Set tn = Nothing

		createThumb = true
	End function

	Public function ValidateImage(sourceFile)
		Dim tn
		' Create instance of AspThumb
		Set tn = openImage(sourceFile)

		If (tn Is Nothing) Then
			ValidateImage = False
			Exit function
		End if

		tn.close

		ValidateImage = true
	End Function


	Public function getImageSize(sourceFile)
		Dim tn, oSize
		' Create instance of AspThumb
		Set tn = openImage(sourceFile)

		Set oSize = new CKFinder_Size

		If (tn Is Nothing) Then
			Set getImageSize = oSize
			Exit function
		End if

		oSize.Width = tn.Width
		oSize.Height = tn.Height

		tn.close
		Set tn = nothing
		Set getImageSize = oSize
	End Function

	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		' Not supported by the component
	End function
End Class


' ServerObjects AspImage Implementation
Class CKFinder_Utils_ImageHandler_AspImage

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	Public Function Enabled()
		Dim component, expireDate

		Enabled = false
		On Error Resume next
		Set component = Server.CreateObject("AspImage.Image")
		If (Err.number=0) Then
			' Check now that it isn't an expired version
			expireDate = component.expires
			If (expireDate="N/A") Then expireDate = #01/01/2050#

			Set component = Nothing
			' It's trickier to find out if it can work or not.
			If (IsNull(expireDate) Or DateDiff("d", expireDate, Now())<0 ) then
				enabled = true
			End If

		End If
		On Error goto 0
	End function


	Public Function Version()
		Dim component

		On Error Resume next
		Set component = Server.CreateObject("AspImage.Image")
		If (Err.number=0) Then
			Version = component.version

			Set component = Nothing
		End If
		On Error goto 0
	End function

	Private Function openImage(sourceFile)
		Dim Image, expireDate
		' Create instance of AspImage
		On Error Resume next
		Set Image = Server.CreateObject("AspImage.Image")

		If (Err.number<>0) Then
			Set Image = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to create AspImage.Image component.", Err.description
			Exit function
		End if
		On Error goto 0

		' Check now that it isn't an expired version
		expireDate = Image.expires
		If (expireDate="N/A") Then expireDate = #01/01/2050#

		' It's trickier to find out if it can work or not.
		If not(IsNull(expireDate) Or DateDiff("d", expireDate, Now())<0 ) then
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "The AspImage.Image component has expired on " & expireDate & ".", ""
			Exit function
		End If

		' Open source image
		If Not(Image.LoadImage(sourceFile)) Then
			Set Image = Nothing
			Set openImage = Nothing
			Exit function
		End If

		Set openImage = Image
	End Function

	' It seems that it can fail to get the dimensions of gif files
	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		Dim Image
		Set Image = openImage(sourceFile)

		If (Image Is Nothing) Then
			createThumb = False
			Exit function
		End If

		Dim oOriginalSize
		Set oOriginalSize = getSize(Image, sourceFile)

		dim iFinalWidth, iFinalHeight
		' If 0 is passed in any of the max sizes it means that that size must be ignored,
		' so the original image size is used.
		iFinalWidth = maxWidth
		If (iFinalWidth = 0) Then iFinalWidth = oOriginalSize.Width
		iFinalHeight = maxHeight
		If (iFinalHeight = 0) Then iFinalHeight = oOriginalSize.Height

		if ( oOriginalSize.Width <= iFinalWidth and oOriginalSize.Height <= iFinalHeight ) then
			Set Image = Nothing
			createThumb = oCKFinder_Factory.UtilsFileSystem.CopyFile( sourceFile, targetFile )
			Exit function
		End If

		dim oNewSize
		if ( preserveAspectRatio ) then
			' Gets the best size for aspect ratio resampling
			Set oNewSize = oCKFinder_Factory.UtilsImage.GetAspectRatioSize( iFinalWidth, iFinalHeight, oOriginalSize.Width, oOriginalSize.Height )
		else
			Set oNewSize = new CKFinder_Size
			oNewSize.Width = iFinalWidth
			oNewSize.Height = iFinalHeight
		End if

		' Resize
		Image.ResizeR oNewSize.Width, oNewSize.Height

		createThumb = saveFile(Image, targetFile, quality)
		Set Image = Nothing
	End Function

	Private function saveFile( Image, targetFile, quality )
		Dim extension
		extension = LCase(oCKFinder_Factory.UtilsFileSystem.GetExtension( targetFile, true ) )
		Select Case extension
			Case "jpg", "jpeg"
				Image.ImageFormat = 1
				Image.JPEGQuality = quality
			Case "bmp"
				Image.ImageFormat = 2
			Case "png"
				Image.ImageFormat = 3
			Case "gif"
				Image.ImageFormat = 5
		End select

		' create thumbnail and save it to disk
		Image.FileName = targetFile
		if Image.SaveImage() then
			saveFile = True
			' It can change automatically the extension to jpg
			If (extension = "jpeg") Then
				Dim savedName
				savedName = Left( targetFile, Len(targetFile)-4) & "jpg"
				saveFile = oCKFinder_Factory.UtilsFileSystem.RenameFile( savedName, targetFile )
			End if
		else
		' Something happened and we couldn't save the image so just use an HTML header
		' We need to debug the script and find out what went wrong.
			saveFile = False
			Dim message
			message = Image.Error
			Set Image = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to save resized image. ", message
			Exit function
		end if
	End Function

	Public function ValidateImage(sourceFile)
		Dim Image
		' Create instance of AspImage
		Set Image = openImage(sourceFile)

		If (Image Is Nothing) Then
			ValidateImage = False
			Exit function
		End If

		Set Image = Nothing
		ValidateImage = true
	End Function

	Public function getImageSize(sourceFile)
		Set getImageSize = getSize(Nothing, sourceFile)
	End Function

	' It won't read the size of gifs
	Public function getSize(Image, sourceFile)
		Dim oSize
		Set oSize = new CKFinder_Size

		If Image is Nothing then
			' Create instance of AspImage
			Set Image = openImage(sourceFile)
			If (Image Is Nothing) Then
				Set getSize = oSize
				Exit function
			End if
			oSize.Width = Image.MaxX
			oSize.Height = Image.MaxY

			Set Image = nothing
		Else
			oSize.Width = Image.MaxX
			oSize.Height = Image.MaxY
		End If

		Set getSize = oSize
	End Function

	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		' Although in theory it's supported, it seems buggy and fails to work
'		Dim Image, logo
'		Set Image = openImage(sourceFile)
'
'		If (Image Is Nothing) Then
'			createThumb = False
'			Exit function
'		End If
'
'		Dim oOriginalSize, oLogoSize
'		Set oOriginalSize = getSize(Image, sourceFile)
'		Set oLogoSize = getImageSize(watermarkFile)
'
'		Dim marginTop
'		marginTop = oOriginalSize.Height - (marginBottom + oLogoSize.Height )
'
'		' doesn't allow positioning or respect the watermark size
'		'Image.DoMerge watermarkFile, 20
'
'		' I've not being able to see it work
'		Image.AddImage watermarkFile, marginTop, marginLeft
'
'		createWatermark = saveFile(Image, sourceFile, quality)
'		Set Image = Nothing
	End function
End class


' ShotGraph Implementation
Class CKFinder_Utils_ImageHandler_ShotGraph

	Private Function throwError( number, text, debugText)
		oCKFinder_Factory.Connector.ErrorHandler.throwError number, text, debugText
	End Function

	Public Function Enabled()
		Dim component

		Enabled = false
		On Error Resume Next
		Set component = Server.CreateObject("shotgraph.image")
		If (Err.number=0) Then
			' Validate that it isn't in demo mode.
			component.CreateImage 1, 1, 32
			If (Err.number=0) then
				Enabled = true
			End if
			Set component = Nothing
		End If
		On Error goto 0
	End function

	Public Function Version()
		' Not provided as of 3.4
		Version = ""
	End function

	Private Function openImage(sourceFile, ByRef imageType, ByRef xSize, ByRef ySize)
		Dim sg
		' Create instance of ShotGraph
		On Error Resume next
		Set sg = Server.CreateObject("shotgraph.image")

		If (Err.number<>0) Then
			Set sg = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Unable to create shotgraph.image component.", Err.description
			Exit function
		End if

		' Validate that it isn't in demo mode.
		sg.CreateImage 1, 1, 32
		If (Err.number<>0) Then
			Set sg = nothing
			throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "The shotgraph.image component has expired.", Err.description
			Exit function
		End If

		' Open source image
		imagetype = sg.GetFileDimensions( sourceFile, xsize, ysize)

		If (Err.number<>0) Or imagetype=0 Or imagetype=4 Or imagetype>=6 Then
			Set sg = nothing
			Set openImage = nothing
			Exit function
		End if
		On Error goto 0

		Set openImage = sg
	End Function

	Public function getImageSize(sourceFile)
		Dim sg, oSize
		Dim palete, imagetype, xsize, ysize
		Set sg = openImage(sourceFile, imagetype, xsize, ysize)

		Set oSize = new CKFinder_Size

		If (sg Is Nothing) Then
			Set getImageSize = oSize
			Exit function
		End If

		oSize.Width = xsize
		oSize.Height =  ysize

		Set sg = nothing
		Set getImageSize = oSize
	End Function

	Public function createThumb(sourceFile, targetFile, maxWidth, maxHeight, quality, preserveAspectRatio)
		Dim sg
		Dim palete, imagetype, xsize, ysize
		Set sg = openImage(sourceFile, imagetype, xsize, ysize)

		If (sg Is Nothing) Then
			createThumb = false
			Exit function
		End If

		dim iFinalWidth, iFinalHeight
		' If 0 is passed in any of the max sizes it means that that size must be ignored,
		' so the original image size is used.
		iFinalWidth = maxWidth
		If (iFinalWidth = 0) Then iFinalWidth = xsize
		iFinalHeight = maxHeight
		If (iFinalHeight = 0) Then iFinalHeight = ysize

		if ( xsize <= iFinalWidth and ysize <= iFinalHeight ) then
			Set sg = nothing
			createThumb = oCKFinder_Factory.UtilsFileSystem.CopyFile( sourceFile, targetFile )
			Exit function
		End If

		dim oSize
		if ( preserveAspectRatio ) then
			' Gets the best size for aspect ratio resampling
			Set oSize = oCKFinder_Factory.UtilsImage.GetAspectRatioSize( iFinalWidth, iFinalHeight, xsize, ysize )
		else
			Set oSize = new CKFinder_Size
			oSize.Width = iFinalWidth
			oSize.Height = iFinalHeight
		End if

		' Resize
		sg.CreateImage oSize.Width, oSize.Height, 32
		sg.InitClipboard xsize, ysize
		sg.SelectClipboard True
		sg.ReadImage sourceFile, palete, 0, 0
		sg.Stretch 0, 0, oSize.Width, oSize.Height, 0, 0, xsize, ysize, "SRCCOPY", "HALFTONE"
		sg.SelectClipboard False
		sg.Sharpen

		' create thumbnail and save it to disk
		Select Case imagetype
			Case 1
				sg.GifImage -1, 0, targetFile
			Case 2
				sg.JpegImage quality, 0, targetFile
			Case 3
				sg.BmpImage 0, targetFile
			Case 5
				sg.PngImage -1, 1, targetFile
		End select

		Set sg = Nothing

		createThumb = true
	End function

	Public function ValidateImage(sourceFile)
		Dim sg
		Dim imagetype, xsize, ysize

		Set sg = openImage(sourceFile, imagetype, xsize, ysize)

		If (sg Is Nothing) Then
			ValidateImage = false
			Exit function
		End If

		Set sg = nothing
		ValidateImage = true
	End function

	Public function createWatermark(sourceFile, watermarkFile, marginLeft, marginBottom, quality, transparency)
		Dim sg, logo, palete
		Dim imagetype, xsize, ysize
		Set sg = openImage(sourceFile, imagetype, xsize, ysize)

		If (sg Is Nothing) Then
			createWatermark = false
			Exit function
		End If

		Dim logoImagetype, logoXsize, logoYsize
		Set logo = openImage(watermarkFile, logoImagetype, logoXsize, logoYsize)

		If (logo Is Nothing) Then
			Set sg = nothing
			createWatermark = false
			Exit function
		End If

		Dim marginTop
		marginTop = ysize - (marginBottom + logoYsize)

		sg.CreateImage xsize, ysize, 32
		sg.ReadImage sourceFile, palete, 0, 0

		sg.ReadImage watermarkFile, palete, marginLeft, marginTop

		' create thumbnail and save it to disk
		Select Case imagetype
			Case 1
				sg.GifImage -1, 0, sourceFile
			Case 2
				sg.JpegImage quality, 0, sourceFile
			Case 3
				sg.BmpImage 0, sourceFile
			Case 5
				sg.PngImage -1, 1, sourceFile
		End select

		Set sg = Nothing

		createWatermark = True
	End function
End Class

</script>
