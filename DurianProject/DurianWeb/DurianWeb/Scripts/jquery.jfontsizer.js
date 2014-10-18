/*
 * 
 * jFontSizer Plugin
 * Written by fluidByte - http://www.fluidbyte.net
 * 
 * 
 */
(function ($) {
    $.fn.jfontsizer = function (o) {

        // Cookie functions
        function setCookie(c_name, value, expiredays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + expiredays);
            document.cookie = c_name + "=" + escape(value) +
	((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
        }

        function getCookie(c_name) {
            if (document.cookie.length > 0) {
                c_start = document.cookie.indexOf(c_name + "=");
                if (c_start != -1) {
                    c_start = c_start + c_name.length + 1;
                    c_end = document.cookie.indexOf(";", c_start);
                    if (c_end == -1) c_end = document.cookie.length;
                    return unescape(document.cookie.substring(c_start, c_end));
                }
            }
            return "";
        }


        // Defaults
        var o = jQuery.extend({
            applyTo: 'body',
            changesmall: '2',
            changelarge: '2',
            expire: 30
        }, o);

        var s = '';
        var m = '';
        var l = '';

        // Current
        var c = 'fs_med';

        // Check cookie  
        if (getCookie('fsizer') != "") {
            var c = getCookie('fsizer');
            switch (c) {
                case 'fs_sml':
                    s = 'fsactive';
                    $(o.applyTo).css('font-size', '.' + (10 - o.changesmall) + 'em');
                    break;
                case 'fs_med':
                    m = 'fsactive';
                    $(o.applyTo).css('font-size', '1em');
                    break;
                case 'fs_lrg':
                    l = 'fsactive';
                    $(o.applyTo).css('font-size', '1.' + o.changelarge + 'em');
                    break;
            }
        }
        else {
            m = "fsactive";
        }

        $(window).keypress(function (e) {
            if (e.which == 43 && e.shiftKey) {
                if (getCookie('fsizer') != "") {
                    var c = getCookie('fsizer');

                    switch (c) {
                        case 'fs_sml':
                            m = 'fsactive';
                            $(o.applyTo).css('font-size', '1em');
                            setCookie('fsizer', 'fs_med', o.expire);
                            c = 'fs_med';
                            break;
                        case 'fs_med':
                            l = 'fsactive';
                            $(o.applyTo).css('font-size', '1.' + o.changelarge + 'em');
                            setCookie('fsizer', 'fs_lrg', o.expire);
                            c = 'fs_lrg';
                            break;
                    }
                }
                else {
                    l = 'fsactive';
                    $(o.applyTo).css('font-size', '1.' + o.changelarge + 'em');
                }
            } else if (e.which == 45 && e.shiftKey) {
                if (getCookie('fsizer') != "") {
                    var c = getCookie('fsizer');
                    switch (c) {
                        case 'fs_med':
                            s = 'fsactive';
                            $(o.applyTo).css('font-size', '.' + (10 - o.changesmall) + 'em');
                            setCookie('fsizer', 'fs_sml', o.expire);
                            c = 'fs_sml';
                            break;
                        case 'fs_lrg':
                            m = 'fsactive';
                            $(o.applyTo).css('font-size', '1em');
                            setCookie('fsizer', 'fs_med', o.expire);
                            c = 'fs_med';
                            break;
                    }
                }
                else {
                    s = "fsactive";
                }
            }
            $('.fsizer a').removeClass('fsactive');
            $('.fsizer a').each(function () {
                if ($(this).attr('id') == c) {
                    $(this).addClass('fsactive');
                }
            });
        });

        // Create font-chooser box
        $(this).html('<div class="fsizer"><a id="fs_sml" class="' + s + '">A</a><a id="fs_med" class="' + m + '">A</a><a id="fs_lrg" class="' + l + '">A</a><br style="clear: both" /></div>');


        $('.fsizer a').click(function () {

            var t = $(this).attr('id');

            setCookie('fsizer', t, o.expire);

            $('.fsizer a').removeClass('fsactive');
            $(this).addClass('fsactive');

            var f = $(o.applyTo).css('font-size');

            switch (t) {
                case 'fs_sml':
                    $(o.applyTo).css('font-size', '.' + (10 - o.changesmall) + 'em');
                    break;
                case 'fs_med':
                    $(o.applyTo).css('font-size', '1em');
                    break;
                case 'fs_lrg':
                    $(o.applyTo).css('font-size', '1.' + o.changelarge + 'em');
                    break;
            }
        });
    }
})(jQuery);