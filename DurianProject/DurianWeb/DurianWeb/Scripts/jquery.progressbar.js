/* File Created: มกราคม 9, 2555 */
// JScript File
(function ($) {

    $.showprogress = function () {
        $.hideprogress();
        $("body").append('<div id="processing_overlay"></div>');
        $("body").append(
		      '<div id="processBar">' +
                '<div class="containerBar">' +
		            '<div class="headerBar">' +
                        'Loading, please wait...</div>' +
    		            '<div class="bodyBar">'+
                        '<img src="http://123.242.134.105/InternalAuditSystem/images/activity.gif" alt="Loading..." />' +
                        '</div></div></div>'
                        );

        var pos = ($.browser.msie && parseInt($.browser.version) <= 6) ? 'absolute' : 'fixed';

        $("#processBar").css({
            position: pos,
            zIndex: 99999,
            padding: 0,
            margin: 0
        });
        /*
        $("#processBar").css({
            minWidth: $("#processBar").outerWidth(),
            maxWidth: $("#processBar").outerWidth()
        });*/

        var top = (($(window).height() / 2) - ($("#processBar").outerHeight() / 2)) + (-75);
        var left = (($(window).width() / 2) - ($("#processBar").outerWidth() / 2)) + 0;
        if (top < 0) top = 0;
        if (left < 0) left = 0;

        // IE6 fix
        if ($.browser.msie && parseInt($.browser.version) <= 6) top = top + $(window).scrollTop();

        $("#processBar").css({
            top: top + 'px',
            left: left + 'px'
        });
        $("#processing_overlay").height($(document).height());
    },
    $.hideprogress = function () {
        $("#processBar").remove();
        $("#processing_overlay").remove();
    },
    $.showmsg = function (msgEle, msgText, msgClass, msgIcon, msgHideIcon, autoHide) {
        var tblMsg;

        tblMsg = '<table width="100%" cellpadding="1" cellspacing="0" border="0" class="' + msgClass + '"><tr><td style="width:30px;" align="center" valign="middle">' + msgIcon + '</td><td>' + msgText + '</td><td style="width:30px;" align="center" valign="middle"><a href="javascript:void(0);" onclick="$(\'#' + msgEle + '\').toggle(400);">' + msgHideIcon + '</a></td></tr></table>';

        $("#" + msgEle).html(tblMsg);
        $("#" + msgEle).show();
        if (autoHide) {
            setTimeout(function () {
                $('#' + msgEle).fadeOut('normal')
            }, 10000
	        );
        }
    }
})(jQuery);

