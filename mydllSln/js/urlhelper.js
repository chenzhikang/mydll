//$.fn.urlhelper.querystring.get()

//调用：$("").geturlp()```,而不是添加到$上
$(function ($) {
    $.fn.geturlp = function (url, pname) {

        var beginpos = url.lastIndexOf("?");
        var allpstr = url.substring(beginpos+1);
        var pstrArr = allpstr.split('&');
        var result = new Array();
        for (var i = 0; i < pstrArr.length; i++) {
            var pstr = pstrArr[i];
            var keyvalue = pstr.split("=");
            result[keyvalue[0]] = keyvalue[1];
        }
        return result[pname];
    }
})(jQuery);
 