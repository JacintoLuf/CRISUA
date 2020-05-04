// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
  

    // sticky footer
    var footer = $("footer");
    if (footer.offset().top + footer.outerHeight(true) < $(window).height()) {
        footer.addClass("sticky");
    }
    // end sticky footer

    // nav menu
    var navInit = function () {
       
        var nav = $(".main.area nav.ua").html();
        if (nav === undefined || nav.length === 0) {
            console.log("menu not found");
        } else {
            $(".system.area nav.ua").html(nav);
        }

        $("nav li").each(function () {
            var t = $(this);
            var link = $("a", t).first();
            if (t.has("ul").length !== 0) {
                var arrow = '<div class="arrow down"></div>';
                var arrowBefore = t.closest("ul").attr("data-arrow") == "before";
                if (arrowBefore) {
                    t.prepend(arrow)
                        .closest("ul")
                        .addClass("multilevel");
                } else {
                    link.after(arrow);
                }
            }

            var path = window.location.pathname; 
            var href = link.attr("href");

            var openArrow = function () {
                for (var i = 0; i < arguments.length; i++) {
                    arguments[i]
                        .find(".arrow")
                        .removeClass("down")
                        .addClass("up");
                }
            };

            if (path === href || href > 1 && path.indexOf(href) > -1) {
                t.addClass("active")
                    .find("ul")
                    .show(); // show child ul

                //show parent ul
                t.closest("ul").show();

                //rotate arrow
                openArrow(t, t.parents("li"));
            }
        });

        $("nav .arrow").click(function (evt) {
            evt.preventDefault();
            var t = $(this);
            var children = t.parent().find("ul");
            if (t.hasClass("up")) {
                t.removeClass("up").addClass("down");
                children.hide("fast");
            } else {
                t.removeClass("down").addClass("up");
                children.show("fast");
            }
        });

        $(".js-menu-toggle").click(function (evt) {
            evt.preventDefault();
            $(".system.area nav.ua").toggle("fast");
        });
    };
    navInit();

    // end nav menu

    // sticky header
    $(window).scroll(function () {
        if ($(window).scrollTop() > 40) {
            $(".system.area").addClass("sticky");
        } else {
            $(".system.area").removeClass("sticky");
        }
    });
    // end sticky header

    $(".js-tab .item").tab();
    $(".dropdown").dropdown();
    $(".ui.progress").progress({ showActivity: false });
    $(".js-modal").click(function (e) {
        e.preventDefault();
        $(".js-modal-example").modal("show");
    });

    $(".ui.accordion").accordion();

    var codeUtils = {
        show: function (code) {
            var cc = $('<code id="code"></code>');
            cc.html(document.createTextNode(code));
            cc.css({
                whitespace: "pre-wrap",
                "margin-left": "10px"
            });

            var msg = $("<div></div>").css({
                "margin-top": "1em",
                "font-style": "italic"
            });

            var content = $('<div class="content"></div>');
            content.append(cc).append(msg);

            var modal = $('<div class="ui modal js-codepen"></div>');
            modal.append('<div class="header">Código</div>').append(content);

            $("body").append(modal);

            modal
                .modal({
                    onHidden: function () {
                        modal.remove();
                    }
                })
                .modal("show");

            var el = document.getElementById("code");
            var range = document.createRange();
            range.selectNodeContents(el);
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
            if (document.execCommand("copy"))
                msg.html("Este código foi copiado para o clipboard.");
            else msg.html("Faça Ctr+C para copiar o código para o clipboard.");
        }
    };

    // demo specific code
    $(".code.example").each(function () {
        var t = $(this);
        var clone = t.clone();
        $(".js-remove", clone).remove();
        var code = clone.html();
        var btn = $(
            '<button class="ui icon button"><i class="code icon"></i></button>'
        );

        btn
            .click(function (e) {
                e.preventDefault();
                codeUtils.show(code);
            })
            .css({
                margin: "0 1em",
                "font-size": "12px",
                "font-weight": "400",
                float: "right"
            });

        var txt = $("<span></span>")
            .text(t.attr("data-text"))
            .css("vertical-align", "middle");

        var d = $('<div class="js-code-bar"></div>')
            .append(txt)
            .append(btn)
            .prependTo(t);

        d.css({
            //'text-align': 'right',
            "font-size": "1.2em",
            "font-weight": "700",
            margin: "3em 0 1em 0",
            "border-top": "2px dashed #ddd",
            "padding-top": "2em"
        });
    });
});
