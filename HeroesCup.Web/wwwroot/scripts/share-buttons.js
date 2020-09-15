$(function () {
    $(document).scroll(function () {
        var $shareButtons = $(".share-buttons");
        var $nav = $('.nav');
        $shareButtons.toggleClass('sticky', $(this).scrollTop() > $nav.height());
    });

    $(document).ready(function () {
        var getWindowOptions = function () {
            var width = 500;
            var height = 450;
            var left = (window.innerWidth / 2) - (width / 2);
            var top = (window.innerHeight / 2) - (height / 2);

            return [
                'resizable,scrollbars,status',
                'height=' + height,
                'width=' + width,
                'left=' + left,
                'top=' + top,
            ].join();
        };

        var fbButton = document.getElementById('fb-share-button');
        var url = window.location.href;

        fbButton.addEventListener('click', function (e) {
            e.preventDefault();
            var win = window.open('https://www.facebook.com/sharer/sharer.php?u=' + url,
                'facebook-share-dialog',
                getWindowOptions()
            );
            win.opener = null;
            return false;
        });

        loadTwitterSdk();
    });

    function loadTwitterSdk() {
        window.twttr = (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0],
                t = window.twttr || {};
            if (d.getElementById(id)) return t;
            js = d.createElement(s);
            js.id = id;
            js.src = "https://platform.twitter.com/widgets.js";
            fjs.parentNode.insertBefore(js, fjs);

            t._e = [];
            t.ready = function (f) {
                t._e.push(f);
            };

            return t;
        }(document, "script", "twitter-wjs"));
    }
});
