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

Class CKFinder_Connector_Upload
	Private oUpload
	Public File
	Public Form
	Public Cookies

	Private Sub Class_Initialize()
		Cookies = Request.Cookies

		Dim aCType
		aCType = Split(Request.ServerVariables("HTTP_CONTENT_TYPE"), ";")
		If uBound(aCType) < 0 Then
			Set Form = Request.Form
		ElseIf aCType(0) <> "multipart/form-data" Then
			Set Form = Request.Form
		Else
			GetUploadedContent()
			Set Form = oUpload.Form
			Set File = oUpload.File
		End If
	End Sub

	Private Sub Class_Terminate()
	End Sub

	Public Property Get ErrNum
		ErrNum = oUpload.ErrNum
	End Property

	Public Sub SaveAs(sItem, sFileName)
		oUpload.SaveAs sItem, sFileName
	End Sub

	Private Sub GetUploadedContent()
		Set oUpload = New NetRube_Upload

		Dim folderHandler
		Set folderHandler = oCKFinder_Factory.FolderHandler

		Dim config
		Set config = oCKFinder_Factory.Config

		Dim resourceTypeConfig
		Set resourceTypeConfig = folderHandler.getResourceTypeConfig()

		Dim checkSizeAfterScaling
		checkSizeAfterScaling = config.getCheckSizeAfterScaling()

		oUpload.MaxSize = 0
		If Not checkSizeAfterScaling Then
			oUpload.MaxSize = resourceTypeConfig.getMaxSize
		End if

		oUpload.Allowed	= resourceTypeConfig.getAllowedExtensions
		oUpload.Denied	= resourceTypeConfig.getDeniedExtensions
		oUpload.HtmlExtensions = config.getHtmlExtensions

		oUpload.GetData()

		If oUpload.ErrNum > 0 Then
			Dim errorHandler
			Set errorHandler = oCKFinder_Factory.Connector.ErrorHandler

			If (oUpload.ErrNum = 1) Then errorHandler.throwError CKFINDER_CONNECTOR_ERROR_UPLOADED_INVALID, False, "Component error in GetData: " & oUpload.ErrNum
			If (oUpload.ErrNum = 2) Then errorHandler.throwError CKFINDER_CONNECTOR_ERROR_UPLOADED_CORRUPT, False, "Component error in GetData: " & oUpload.ErrNum
			If (oUpload.ErrNum = 3) Then errorHandler.throwError CKFINDER_CONNECTOR_ERROR_UPLOADED_TOO_BIG, False, "Component error in GetData: " & oUpload.ErrNum

			If (oUpload.ErrNum = 7) Then errorHandler.throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, "Corrupted data", "Component error in GetData: " & oUpload.ErrNum

			errorHandler.throwError CKFINDER_CONNECTOR_ERROR_CUSTOM_ERROR, oUpload.ErrDescription, "Component error in GetData: " & oUpload.ErrNum
		End If
	End Sub
End Class

</script>
