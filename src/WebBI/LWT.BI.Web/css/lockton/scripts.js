$(document).ready(function() {
	// MEGA MENU SCRIPTS
	var Menu = function(id) {
        var menu = {};
 
        menu.id = id;
        menu.item = $('.menu#' + menu.id);
        menu.links = $('.dropdown#dropdown' + menu.id);
 
        menu.show = function() {
            menu.hideOthers();
            menu.item.addClass('hover');
            menu.links.addClass('active');
        }
 
        menu.hide = function() {
        	//debugger;
    		menu.item.removeClass('hover');
            menu.links.removeClass('active');
        }
 
        menu.hideOthers = function() {
            var i, _i, _len;
            var menus = window.menus;
            for (_i = 0, _len = menus.length; _i < _len; _i++) {
              i = menus[_i];

              if (i.id != menu.id) { i.hide(); }
            }
        }
 
        menu.bindHovers = function() {
            $.merge(menu.item, menu.links).hover(function() {
                menu.show();
            }, function() {
				menu.hide();
            });
        }
 
        menu.bindHovers();
        return menu;
    }
 
    window.menus = []
    $('.menu').each(function() {
        var id = $(this).attr('id');
        menus.push(Menu(id));
    });

    if ($("body").hasClass("interior")) {
        //VIDEO DIV APPENDER
        var $allVideos = $("iframe[src^='http://player.vimeo.com'], iframe[src^='http://www.youtube.com']"),

            $fluidEl = $(".media");

        $allVideos.each(function() {

          $(this)
            .data('aspectRatio', this.height / this.width)

            .removeAttr('height')
            .removeAttr('width');

        });

        $(window).resize(function() {

          var newWidth = $fluidEl.width();

          $allVideos.each(function() {

            var $el = $(this);
            $el
              .width(newWidth)
              .height(newWidth * $el.data('aspectRatio'));

          });

        }).resize();

        //RELATED PUBLICATIONS
        if (!$("body").hasClass("insights")) {
	        $('#related_carousel').liquidcarousel({
	            height: 285,        //the height of the list
	            duration: 100,      //the duration of the animation
	            hidearrows: true    //hide arrows if all of the list items are visible
	        });
        }

        if (!$("body").hasClass("insights")) {
            //SIDE AFFIXED MENU
            $('#sidenav').sticky({
                topSpacing: 160,
                bottomSpacing: 315,
                wrapperClassName: "nav_container",
            });
        }

        //PAGE SCROLLING
        $('a[href*=#]:not([href=#])').click(function() {
            if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') 
                || location.hostname == this.hostname) {

                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
                   if (target.length) {
                     $('html,body').animate({
                         scrollTop: target.offset().top - 100
                    }, 400);
                    return false;
                }
            }
        });
    }

	//ASYNCH SLIDER FOR HOMEPAGE
	if ($("body").hasClass("home")) {
        $(".client_slides").responsiveSlides({
            auto: true,
            timeout: 8000,
            pager: true,
            nav: true,
        });
	}

    if ($("body").hasClass("office")) {
        $(".office_slides").responsiveSlides({
            auto: true,
            timeout: 8000,
            pager: true,
            nav: true,
        });
    }
});