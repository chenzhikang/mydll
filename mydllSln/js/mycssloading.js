/*
javascript mycssloading.js
name:jq css loading  插件
author:abc
date:2016-8-23 14:02:01
version:1.0.*
license:MIT
*/
(function ($) {
    $.fn.mycssloading = function (options) {
        //Defaults setting
        var settings = $.extend(
            {
                speed: 1000,
                width: "100%",
                height: "100%",
                colorvalue: "#d22222",
                style: "default",
            }, options
            );
        var $this = $(this);
        var loadingwrapper = '<div class="spinner"></div>';
        var rect1 = ' <div class="rect1"></div>';
        var rect2 = ' <div class="rect2"></div>';
        var rect3 = ' <div class="rect3"></div>';
        var rect4 = ' <div class="rect4"></div>';
        var rect5 = ' <div class="rect5"></div>';

        var $htmlobj = $(loadingwrapper).height(settings.height).append($(rect1 + rect2 + rect3 + rect4 + rect5));
        $this.append($htmlobj);

        //todo:return;//返回自身对象，用来调用方法、绑定事件等
    };
})(jQuery);