/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.systaxhighlight_lang = 'csharp';
    config.systaxhighlight_hideControls = true;
    config.language = 'vi';
    config.htmlEncodeOutput = true;
  
    config.filebrowserBrowseUrl = '$"wwwroot/assets/admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '$"wwwroot/assets/admin/js/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '$"wwwroot/assets/admin/js/plugins/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '$"wwwroot/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '$"wwwroot/assest/images';
    config.filebrowserFlashUploadUrl = '$"wwwroot/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '$"wwwroot/assets/admin/js/plugins/ckfinder/')
};
