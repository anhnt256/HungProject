var path_resource = "http://" + location.host + "/assets/";

var pUpload = {
    init: function () {
        console.log("<pUpload> => INIT!");

        PIXIApp.init("canvas");
        //$('.js_gotoUpload').on('click', pUpload.onClickShowUploadPicture );
        $('.js_show__result > a.skip').on('click', pUpload.onClickShowResult);

        $('.js_dot li').on('click', function () {
            var i = $(this).index();
            $('.js_sticker__slider > div').hide();
            $('.js_sticker__slider > div:eq(' + i + ')').show();
        })

        // $('.js_sticker__slider').slick({
        //   infinite: false,
        //   slidesToShow: 1,
        //   slidesToScroll: 1,
        //   arrows: false,
        //   dots: true,
        // });

    },

    onClickShowUploadPicture: function (e) {
        // e.preventDefault();

        var tl = new TimelineLite();
        $('.up_step li').removeClass('valid');
        $('.up_step li:eq(1)').addClass('valid');

        TweenMax.set('.upload_wrap h2, .interact h3', { autoAlpha: 0, y: -50 });
        TweenMax.set('.app-wrap .upload ', { autoAlpha: 0, x: 50 });
        TweenMax.set('.upload_wrap', { css: { zIndex: 10, autoAlpha: 1, visible: 'visible' } })
        TweenMax.set('#frame li, #sticker li', { scale: 0 })
        TweenMax.set('.interact .ip, .buttons', { y: 50, autoAlpha: 0 })
        TweenMax.set('.up_step li', { scale: 0 })

        tl
            .staggerTo('.up_step li', 1, { scale: 1, visibility: 'visible', ease: Back.easeOut }, .02, 's')
            .to('.up_background > div', 1, { x: '15%', ease: Expo.easeOut }, 's')
            .to('.upload_wrap h2', 1, { autoAlpha: 1, y: 0, ease: Expo.easeOut }, 's')
            .to('.app-wrap .upload', 1, { autoAlpha: 1, x: 0, ease: Expo.easeOut }, 's+=.7')
            .to('#frame h3', 1, { autoAlpha: 1, y: 0, ease: Expo.easeOut }, 's+=.7')
            .staggerTo('#frame li', 1, { scale: 1, ease: Expo.easeOut }, .05, 's+=.7')
            .to('#sticker h3', 1, { autoAlpha: 1, y: 0, ease: Expo.easeOut }, 's+=1.5')
            .staggerTo('#sticker li', 1, { scale: 1, ease: Expo.easeOut }, .05, 's+=1.5')
            .to('.interact .ip', 1, { y: 0, autoAlpha: 1, ease: Expo.easeOut }, 's+=2.5')
            .to('.buttons', 1, { y: 0, autoAlpha: 1, ease: Expo.easeOut })
            ;
    },

    onClickShowResult: function (e) {
        //e.preventDefault();

        var tl = new TimelineLite();
        $('.up_step li').removeClass('valid');
        $('.up_step li:eq(2)').addClass('valid');
        tl
            .to('.upload_wrap', 1, { autoAlpha: 0, ease: Expo.easeOut })
            .set('.upload_wrap ', { display: 'none' })
            .set('.result_wrap ', { display: 'block' })
            .to('.result_wrap', 1, { autoAlpha: 1, ease: Expo.easeOut })
            // .to('.upload_wrap' , 1, { css:{zIndex:10, autoAlpha: 1 } })
            ;
        $('.up_background > div').addClass('bg_result');

    }

}
var PIXIApp = {
    
    // pixi vars:

    renderer: null,
    canvas: null,
    center: null,
    stage: null,
    loader: null,
    autoSize: "fixsize", // flexible, fixsize
    globalScale: { x: 1, y: 1 },
    backgroundColor: 0x000000,
    transparent: false,
    canvasSize: { width: 0, height: 0 },
    canvasHolderClass: "canvasHolder",

    isInit: false,

    // app variables:

    assetsArr: [
        // frames
        { name: "frame-0", url: path_resource + "images/frame/1.png" },
        { name: "frame-1", url: path_resource + "images/frame/2.png" },
        { name: "frame-2", url: path_resource + "images/frame/3.png" },
        { name: "frame-3", url: path_resource + "images/frame/4.png" },
        { name: "frame-4", url: path_resource + "images/frame/5.png" },
        { name: "frame-5", url: path_resource + "images/frame/6.png" },
        // watermarks
        //{ name: "textMarker", url: path_resource + "images/sticker/14.png" },
        //{ name: "marker", url: path_resource + "images/watermark/marker.png" },
        { name: "marker-0", url: path_resource + "images/watermark/1.png" },
        { name: "marker-1", url: path_resource + "images/watermark/2.png" },
        { name: "marker-2", url: path_resource + "images/watermark/3.png" }
    ],

    activeStickerArr: [],
    frameArr: ["frame-0", "frame-1", "frame-2", "frame-3", "frame-4", "frame-5", "frame-6", "frame-7"],

    isUploadedPicture: false,
    // functions

    init: function (_canvasHolderClass) {
        var scope = PIXIApp;

        if (_canvasHolderClass) scope.canvasHolderClass = _canvasHolderClass;

        if (scope.autoSize == "fixsize") {
            // customize css:
            $("." + scope.canvasHolderClass).css({
                "position": "absolute",
                "top": "0",
                "left": "0",
                "width": "100%",
                "height": "100%",
                "overflow": "hidden"
            })
            $("." + scope.canvasHolderClass + " canvas").css({
                "transform-origin": "top left",
                "-webkit-transform-origin": "top left",
                "-moz-transform-origin": "top left",
                "-ms-transform-origin": "top left"
            })
        }

        scope.canvas = $("." + scope.canvasHolderClass + " canvas")[0];
        scope.center = {}
        scope.canvasSize.width = scope.canvas.width;
        scope.canvasSize.height = scope.canvas.height;

        // setup PIXI
        var scale = window.devicePixelRatio;
        //alert(scale);

        scope.renderer = PIXI.autoDetectRenderer(scope.canvas.width, scope.canvas.height, { view: scope.canvas, transparent: scope.transparent, backgroundColor: scope.backgroundColor, antialias: true });
        scope.stage = new PIXI.Container();

        scope.renderType = (scope.renderer instanceof PIXI.CanvasRenderer) ? "canvas" : "webgl";


        scope.stage.interactive = true;

        // === preload images ===

        scope.loader = PIXI.loader;

        //-- start load assets --

        if (scope.assetsArr.length == 0) {
            scope.onAssetsLoaded();
        } else {
            for (var i = 0; i < scope.assetsArr.length; i++) {
                scope.loader.add(scope.assetsArr[i].name, scope.assetsArr[i].url);
            }
            PIXI.loader.on('progress', scope.onLoadProgress);
        }

        //-- end load assets --

        scope.loader.once('complete', scope.onAssetsLoaded);
        scope.loader.load();


        // === render ===

        requestAnimationFrame(animate);

        function animate() {
            scope.onRuntime();
            scope.renderer.render(scope.stage);
            requestAnimationFrame(animate);
        }
    },
    onLoadProgress: function () {

    },
    onAssetsLoaded: function () {
        console.log("[PIXIApp] All assets are loaded! ");
        var scope = PIXIApp;

        var fileSize = 10 * 1000 * 1000; // 10MB
        var fileTypes = ['image/png', 'image/jpeg'];
        var isFileValid = true;


        function imageValidate(file) {
            console.log(file);
            //console.log('file', file);
            if (!file || fileTypes.indexOf(file.type) < 0) {
                alert("Vui lòng upload ảnh đúng định dạng jpg hay png");
                return false;
            }
            else if (file.size > fileSize) {
                alert("Vui lòng upload ảnh có dung lượng thấp hơn 10mb");
                return false;
            }
            return true;
        }


        $(".js_upload").click(function (e) {
            e.preventDefault();

            isFileValid = true;

            if (!GUpload.onStart) {
                GUpload.onStart = function () {
                    //$(".img-preloader").addClass('display');
                    //TweenMax.to($(".img-preloader .spin"), 1, {rotation: 360, ease: Linear.easeNone, repeat: -1});
                }
            }

            if (!GUpload.onCancel) {
                GUpload.onCancel = function () {
                    // TweenMax.killTweensOf($(".img-preloader .spin"));
                    // $(".img-preloader").removeClass('display');
                }
            }

            GUpload.browse(function (result, params) {
                console.log(GUpload.fileMetaData);
                if (!imageValidate(GUpload.fileMetaData)) return;
                //$(".orig").attr("src", base64);

                scope.loadPicture(result, params, function () {
                    //console.log("-> base64 loaded...")

                    //   TweenMax.killTweensOf($(".img-preloader .spin"));
                    //   $(".img-preloader").removeClass('display');
                    $('.js_upload.first').remove();
                    scope.startCropPicture();

                });
                //TweenMax.killTweensOf($(".img-preloader .spin"));
            })

            //ga('send', 'event', 'Do Key Actions', '', '2A. Tải Ảnh Từ máy');
        });

        var ghostItem;
        var selectedItem;
        var stickerDom = $("#sticker").find(".list_sticker");
        stickerDom.find("li img").attr("draggable", "false");
        stickerDom.find("li").on("mousedown touchdown", startDrag);

        function startDrag(e) {
            $(window).on("mouseup touchup", stopDrag);
            $(window).on("mousemove touchmove", onDrag);
            var mouse = GMouse.getGlobal(e);

            selectedItem = $(this).find("img");

            if (!ghostItem) {
                $('.btn-delete').show();
                $("body").append('<img class="ghost" draggable="false" src="' + $(this).find("img").attr("src") + '">');
                ghostItem = $(".ghost");
                ghostItem.css({
                    "position": "absolute",
                    "width": "80px",
                    "height": "auto",
                    "z-index": "1000",
                    "transform": "translate(-50%, -50%)",
                    "-webkit-transform": "translate(-50%, -50%)",
                    "-moz-transform": "translate(-50%, -50%)",
                    "-ms-transform": "translate(-50%, -50%)",
                    "left": mouse.x + "px",
                    "top": mouse.y + "px"
                })
            }
        }
        function onDrag(e) {
            var mouse = GMouse.getGlobal(e);
            ghostItem.css({
                "left": mouse.x + "px",
                "top": mouse.y + "px"
            })
        }
        function stopDrag(e) {
            var mouse = GMouse.getLocal(e, $(".upload .canvas"));
            //console.log(mouse);
            scope.addSticker(selectedItem.attr("src"), mouse);

            $(window).off("mouseup touchup", stopDrag);
            $(window).off("mousemove touchmove", onDrag);

            $(".ghost").remove();
            ghostItem = null;
        }

        // FRAME:
        $("#frame li").click(function (e) {
            var id = $(this).index();
            scope.loadFrame(scope.frameArr[id]);
        });

        $('.interact-contain li').on('click', function () {
            $(this).parents('.interact-contain').find('li').removeClass('selected');
            $(this).addClass('selected');
        })

        // DELETE STICKER:

        $(".btn-delete").click(function (e) {
            if (scope.activeSticker) {
                if (scope.activeSticker.name != "slogan") {
                    scope.transformTool.clear();
                    GArray.remove(scope.activeSticker, scope.activeStickerArr);
                    GPixi.remove(scope.activeSticker);
                    scope.activeSticker = null;
                } else {
                    alert("Bạn không thể xóa nhãn này.")
                }
            }
        });


        $(".btn-zoomin").click(function (e) {
            e.preventDefault();
            scope.zoomIn();
            scope.checkPhotoPosition();
        })

        $(".btn-zoomout").click(function (e) {
            e.preventDefault();
            scope.zoomOut();
            scope.checkPhotoPosition();
        })

        $(".btn-rotate").click(function (e) {
            e.preventDefault();
            scope.rotate();
        })

        function b64toBlob(b64Data, contentType, sliceSize) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512;

            var byteCharacters = atob(b64Data);
            var byteArrays = [];

            for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
                var slice = byteCharacters.slice(offset, offset + sliceSize);

                var byteNumbers = new Array(slice.length);
                for (var i = 0; i < slice.length; i++) {
                    byteNumbers[i] = slice.charCodeAt(i);
                }

                var byteArray = new Uint8Array(byteNumbers);

                byteArrays.push(byteArray);
            }

            return new Blob(byteArrays, { type: contentType });
        }

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
        }

        // SUBMIT

        $(".js_review").click(function (evt) {
            evt.preventDefault();

            if ($(this).hasClass('display')) {

                if (!scope.isUploadedPicture) {
                    alert("Vui lòng tải hình lên để tham gia.");
                    return;
                }

                scope.transformTool.clear();
                scope.renderer.render(scope.stage);
                //var exportBase64 = scope.canvas.toDataURL();
                var exportBase64 = GPixi.containerToBase64(scope.renderer, scope.stage, { width: scope.canvas.width, height: scope.canvas.height });
                $('.review_thumb').attr('src', exportBase64);
                var b = $('#frame li.selected').index();
                var t = $('.js_your_comment').val();

                if (t == '') {
                    t = 'Tôi yêu xe ga Yamaha!'
                }

                if (b <= 3 && b >= 2) {
                    $('.review_wrap .text').addClass('blue');
                } else if (b <= 5 && b >= 4) {
                    $('.review_wrap .text').addClass('red');
                }

                $('.review_wrap .text').text(t);


                // var tl = new TimelineLite();
                // tl.to('.upload', 1, { x: -200 , autoAlpha: 0 , ease:Power2.easeOut },'s')
                //   .to('.interact', 1, { x: 200 , autoAlpha: 0, ease:Power2.easeOut },'s')
                //   .to('.area-upload .buttons', 1, { y: 50 , autoAlpha: 0, ease:Power2.easeOut },'s')
                //   .set('.area-upload', { display: 'none'})
                //   .set('.review_wrap', { display: 'block'})
                //   .to('.review_wrap', 1, { autoAlpha: 1, ease:Power2.easeOut })
            }

            // Get the form element withot jQuery
            var block = exportBase64.split(";");
            var contentType = block[0].split(":")[1];// In this case "image/gif"
            var realData = block[1].split(",")[1];// In this case "R0lGODlhPQBEAPeoAJosM...."
            var blob = b64toBlob(realData, contentType);

            console.log(blob);

            var formDataToUpload = new FormData();
            formDataToUpload.append("file", blob, guid() + '.jpg');
            formDataToUpload.append("share_text", $('.js_your_comment').val());
            formDataToUpload.append("color", $('#frame > ul > li.selected').attr('data-frame'));

            console.log(formDataToUpload);

            // var base64ImageContent = exportBase64.replace(/^data:image\/(png|jpg);base64,/, "");
            // var blob = base64ToBlob(base64ImageContent, 'image/png');
            // var formData = new FormData();
            // formData.append('picture', blob);

            $(this).removeClass('display');

            $.ajax({
                url:'/Home/UploadImage',
                type: 'POST',
                method: 'POST',
                data: formDataToUpload,
                cache: false,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (data) {

                    if (typeof congratulationText != "undefined"); //congratulationText()

                    // data = JSON.parse(data);

                    if (String(data.status) == "1") {

                        $('.share_link').attr('href', data.share_link)

                        var tl = new TimelineLite();
                        tl.to('.upload', 1, { x: -200, autoAlpha: 0, ease: Power2.easeOut }, 's')
                            .to('.interact', 1, { x: 200, autoAlpha: 0, ease: Power2.easeOut }, 's')
                            .to('.area-upload .buttons', 1, { y: 50, autoAlpha: 0, ease: Power2.easeOut }, 's')
                            .set('.area-upload', { display: 'none' })
                            .set('.review_wrap', { display: 'block' })
                            .to('.review_wrap', 1, { autoAlpha: 1, ease: Power2.easeOut });

                        if (data.result == '1') {
                            $('.resulte_thumb > img').attr('src', data.image_product);
                            $('.js_name__gift').text(data.product_name);
                            $('.count > h6').html(data.play_count + '<span>/</span>' + data.max_play);
                            window.history.pushState({}, 'Upload successfully', '/upload/success');
                        } else {
                            $('.winner').hide();
                            $('.count > h6').html(data.play_count + '<span>/</span>' + data.max_play);
                            $('.loser').show();
                            window.history.pushState({}, 'Upload successfully', '/upload/success');
                        }

                        if (data.message != '') {
                            alert(data.message);
                        }

                    } else {
                        var message = '';
                        $.each(data.message, function (k, value) {
                            message = message + value + '\n';
                        });
                        $(".js_review").addClass('display');
                        alert(message);

                    }
                }, error: function (e) {
                    alert("Đã xảy ra lỗi ! Xin vui lòng nhấn F5 và thử lại.");
                    //window.location.reload();
                }
            });



            //ga('send', 'event', 'Do Key Actions', '', '2C. Xem Poster Của Bạn');
        });

        //   $('.share_link').click(function(e) {
        //       e.preventDefault();

        //       window.open('https://www.facebook.com/sharer.php?u=' + $(this).attr('href'), 'fbShareWindow', 'height=450, width=550, top=' + ($(window).height() / 2 - 275) + ', left=' + ($(window).width() / 2 - 225) + ', toolbar=0, location=0, menubar=0, directories=0, scrollbars=0');
        //       pUpload.onClickShowResult();
        //       return false;
        //   });



        var bottomLayer = new PIXI.Graphics();
        bottomLayer.beginFill(0x000000, 0);
        bottomLayer.drawRect(0, 0, PIXIApp.canvas.width, PIXIApp.canvas.height);
        scope.stage.addChild(bottomLayer);

        scope.bottomLayer = bottomLayer;

        bottomLayer.interactive = true;
        bottomLayer.on("click", deselectItem);
        //bottomLayer.on("touchend", deselectItem);
        bottomLayer.on("touchendoutside", deselectItem);

        function deselectItem() {
            transformTool.clear();
            scope.activeSticker = null;
        }

        scope.stickerHolder = new PIXI.Container();
        scope.stage.addChild(scope.stickerHolder);

        var transformTool = new PIXI.TransformTool({
            debug: false,
            canvas: scope.canvas,
            scaleByRatio: true,
            relativeScale: scope.globalScale.x,
            lockReg: true,
            border: true
        });

        scope.transformTool = transformTool;
        scope.stickerHolder.addChild(transformTool);

        //--- FRAME ---

        var frame = new PIXI.Container();
        scope.frame = frame;
        scope.stage.addChild(frame);


        function setSloganColor(id) {
            //console.log("setSloganColor: ", id);
            for (var i = 0; i < slogan.children.length; i++) {
                var mc = slogan.children[i];
                if (mc.id == id) {
                    mc.visible = true;
                } else {
                    mc.visible = false;
                }
            }
        }

        GPixi.moveAboveItem(bottomLayer, frame);
        GPixi.moveToTop(scope.stickerHolder);

        // CROP:

        $(".btn-crop").on("click", function (e) {
            if ($(this).hasClass('selected')) {
                scope.finishCropPicture();
            } else {
                scope.startCropPicture();
            }
        })

        //--- ANIMATE IN ---
        scope.loadPicture($(".upload .orig").attr("src"), { isLoadingSample: true }, function () {
            TweenMax.to($(".area-upload .step"), .3, { opacity: 1, ease: Quart.easeOut });
            scope.finishCropPicture();
            scope.loadFrame(scope.frameArr[0]);

            pUpload.onClickShowUploadPicture();

        });


        scope.isInit = true;
        scope.checkCanvasSizeChange();

    },

    addSticker: function (url, position) {
        //console.log("addSticker", url, position)
        var scope = PIXIApp;

        scope.finishCropPicture();

        var loader = new PIXI.loaders.Loader();
        loader.add("sticker", url);
        loader.once("complete", function () {
            //console.log("complete", scope.activeStickerArr.length)
            var sticker = GPixi.addNewSpriteTo("sticker" + scope.activeStickerArr.length, scope.stickerHolder, "sticker", this);
            sticker.interactive = true;
            sticker.buttonMode = true;

            var originalSize = GPixi.getOriginalSize(sticker);
            sticker.pivot.set(originalSize.width / 2, originalSize.height / 2);

            //scope.stickerHolder.addChild(sticker);
            scope.activeStickerArr.push(sticker);

            sticker.width = 200;
            sticker.scale.y = sticker.scale.x;

            sticker.x = position.x / scope.globalScale.x;
            sticker.y = position.y / scope.globalScale.y;

            if (sticker.x > scope.canvas.width || sticker.x < 0) {
                sticker.x = scope.canvas.width * 0.2 + GMath.random(scope.canvas.width * 0.6);
            }
            if (sticker.y > scope.canvas.height || sticker.y < 0) {
                sticker.y = scope.canvas.height * 0.2 + GMath.random(scope.canvas.height * 0.6);
            }

            //-- move slogan to top --

            //GPixi.moveToTop(scope.slogan);

            //-- interaction --
            scope.activeSticker = sticker;
            scope.transformTool.apply(sticker);
            sticker.on("mousedown", stickerClick);
            sticker.on("touchstart", stickerClick);

            function stickerClick() {
                scope.activeSticker = this;
                scope.transformTool.apply(this);
            }
        })
        loader.load();
    },

    loadFrame: function (textureId) {
        var scope = PIXIApp;

        scope.finishCropPicture();

        if (scope.frame.frame) {
            GPixi.remove(scope.frame.frame);
            scope.frame.frame = null;
        }
        var frame = GPixi.addNewSpriteTo("frame", scope.frame, textureId);

        scope.frame.width = scope.canvas.width;
        scope.frame.height = scope.canvas.height;

        GPixi.moveToTop(scope.stickerHolder);
    },

    startCropPicture: function () {
        var scope = PIXIApp;

        /*if(Math.round(scope.photoAlpha.width) == Math.round(scope.photoAlpha.height))
        {
            console.log("[CROP PICTURE] Not available. Picture was already in square.");
            scope.finishCropPicture();
            return;
        }*/

        $(".btn-zoomin, .btn-zoomout, .btn-rotate").addClass('display');
        $(".crop-instruction").addClass('display');
        $(".btn-crop").addClass('selected')
        $(".btn-crop span").html("Hoàn tất chỉnh sửa ảnh");
        $(".js_review").removeClass('display');
        $(".btn-delete").hide();
        //$('.js_review').removeClass('disable');
        scope.photoContainer.scale.set(1, 1);
        scope.photoAlpha.interactive = true;
        scope.photoAlpha.buttonMode = true;
        //scope.marker.visible = false;
        scope.frame.visible = false;
        scope.stickerHolder.visible = false;
        //GPixi.moveToTop(scope.frame);
    },

    finishCropPicture: function (callback) {
        var scope = PIXIApp;
        $(".btn-zoomin, .btn-zoomout, .btn-rotate").removeClass('display');
        $(".crop-instruction").removeClass('display');
        $(".btn-crop").removeClass('selected');
        $(".btn-crop span").html("Chỉnh vị trí");
        $(".btn-color").addClass("display");
        $(".js_review").addClass('display');
        scope.photoContainer.scale.set(1, 1);
        scope.photoAlpha.interactive = false;
        scope.photoAlpha.buttonMode = false;
        scope.photoAlpha.visible = false;
        //scope.marker.visible = true;
        scope.frame.visible = true;
        scope.stickerHolder.visible = true;

        scope.photoContainer.boundMsk.scale.x = 2;
        //scope.photo.mask = null;
        //scope.photoContainer.boundMsk.visible = false;

        GPixi.moveToTop(scope.frame);
        GPixi.moveToTop(scope.bottomLayer);
        //GPixi.moveToTop(scope.marker);
        GPixi.moveToTop(scope.stickerHolder);

    },

    checkPhotoPosition: function (detectImg) {

        var scope = PIXIApp;

        if (scope.photoAlpha.x - scope.photoAlpha.width / 2 > -scope.canvas.width / 2) {
            scope.photoAlpha.x = scope.photoAlpha.width / 2 - scope.canvas.width / 2;
        }
        if (scope.photoAlpha.x + scope.photoAlpha.width / 2 < scope.canvas.width / 2) {
            scope.photoAlpha.x = scope.canvas.width / 2 - scope.photoAlpha.width / 2;
        }
        if (scope.photoAlpha.y - scope.photoAlpha.height / 2 > -scope.canvas.height / 2) {
            scope.photoAlpha.y = scope.photoAlpha.height / 2 - scope.canvas.height / 2;
        }
        if (scope.photoAlpha.y + scope.photoAlpha.height / 2 < scope.canvas.height / 2) {
            scope.photoAlpha.y = scope.canvas.height / 2 - scope.photoAlpha.height / 2;
        }


        scope.photo.x = scope.photoAlpha.x;
        scope.photo.y = scope.photoAlpha.y;
    },

    zoomIn: function () {
        var scope = PIXIApp;
        scope.photoAlpha.width += 100;
        scope.photoAlpha.scale.y = scope.photoAlpha.scale.x;
        if (scope.photoAlpha.width > scope.canvas.width * 3) {
            scope.photoAlpha.width = scope.canvas.width * 3;
            scope.photoAlpha.scale.y = scope.photoAlpha.scale.x;
        }
        if (scope.photoAlpha.height > scope.canvas.height * 3) {
            scope.photoAlpha.height = scope.canvas.height * 3;
            scope.photoAlpha.scale.x = scope.photoAlpha.scale.y;
        }
        scope.photo.scale.x = scope.photoAlpha.scale.x;
        scope.photo.scale.y = scope.photoAlpha.scale.y;

    },

    zoomOut: function () {
        var scope = PIXIApp;
        scope.photoAlpha.width -= 100;
        scope.photoAlpha.scale.y = scope.photoAlpha.scale.x;
        if (scope.photoAlpha.width < scope.canvas.width) {
            scope.photoAlpha.width = scope.canvas.width;
            scope.photoAlpha.scale.y = scope.photoAlpha.scale.x;
        }
        if (scope.photoAlpha.height < scope.canvas.height) {
            scope.photoAlpha.height = scope.canvas.height
            scope.photoAlpha.scale.x = scope.photoAlpha.scale.y;
        }
        scope.photo.scale.x = scope.photoAlpha.scale.x;
        scope.photo.scale.y = scope.photoAlpha.scale.y;
    },

    rotate: function () {

        var scope = PIXIApp;

        scope.photoContainer.rotation += (Math.PI * 2 * 0.125) * 2;

        //scope.photoAlpha.x = (scope.canvas.width - scope.canvas.width )/2;
        //scope.photo.x = scope.photoAlpha.x;

        // var xImg = '';

        // if( scope.photoAlpha.width < scope.photoAlpha.height ){
        //     xImg = 'vertical';
        // } else {
        //     xImg = 'horizontal';
        // }

        scope.checkPhotoPosition();

    },

    loadPicture: function (url, params, callback) {
        var scope = PIXIApp;

        var loader = new PIXI.loaders.Loader();
        loader.add("picture", url);
        loader.once('complete', onPhotoLoaded);
        loader.load();

        //$(".btn-color").removeClass("display");

        function onPhotoLoaded() {
            if (scope.photo) {
                GPixi.remove(scope.photo);
            }
            if (scope.photoContainer) {
                GPixi.remove(scope.photoContainer);
            }

            if (params && typeof params.isLoadingSample != "undefined") {
                if (!params.isLoadingSample) {
                    scope.isUploadedPicture = true;
                } else {
                    scope.isUploadedPicture = false;
                }
            } else {
                scope.isUploadedPicture = true;
            }

            scope.photoContainer = new PIXI.Container();
            scope.stage.addChild(scope.photoContainer);
            scope.photoContainer.x = scope.canvas.width / 2;
            scope.photoContainer.y = scope.canvas.height / 2;
            //scope.photoContainer.scale.set(0.5, 0.5);

            scope.photo = GPixi.addNewSpriteTo("photo", scope.photoContainer, "picture", this);
            //GPixi.moveToBottom(scope.photo);

            scope.photo.anchor.set(0.5, 0.5);

            scope.photo.width = scope.canvas.width;
            scope.photo.scale.y = scope.photo.scale.x;

            if (scope.photo.height < scope.canvas.height) {
                scope.photo.height = scope.canvas.height;
                scope.photo.scale.x = scope.photo.scale.y;
            }

            //scope.photo.x = scope.canvas.width/2;
            //scope.photo.y = scope.canvas.height/2;

            // alpha photo
            scope.photoAlpha = GPixi.addNewSpriteTo("photoAlpha", scope.photoContainer, "picture", this);
            scope.photoAlpha.anchor.set(0.5, 0.5);
            scope.photoAlpha.x = scope.photo.x;
            scope.photoAlpha.y = scope.photo.y;
            scope.photoAlpha.alpha = .7;
            scope.photoAlpha.width = scope.photo.width;
            scope.photoAlpha.height = scope.photo.height;
            //GPixi.moveToBottom(scope.photoAlpha);

            //boundShape
            var boundMsk = new PIXI.Graphics();
            boundMsk.beginFill(0xFFFFFF, 1);
            boundMsk.drawRect(0, 0, scope.canvas.width, scope.canvas.height);
            boundMsk.pivot.set(scope.canvas.width / 2, scope.canvas.height / 2);

            scope.photoContainer.addChild(boundMsk);
            scope.photoContainer.boundMsk = boundMsk;

            scope.photo.mask = boundMsk;

            GPixi.moveToTop(scope.photoAlpha);

            // drag&drop

            scope.photoAlpha.interactive = true;
            scope.photoAlpha.buttonMode = true;


            scope.photoAlpha.on("mousedown", onDown);
            scope.photoAlpha.on("touchstart", onDown);

            var startPos = { x: 0, y: 0 }
            var startPhotoPos = { x: 0, y: 0 }

            function onDown(e) {
                startPos = e.data.getLocalPosition(scope.photoContainer);
                startPhotoPos = { x: scope.photoAlpha.x, y: scope.photoAlpha.y }
                scope.photoAlpha.on("mousemove", onMove);
                scope.photoAlpha.on("touchmove", onMove);
                scope.photoAlpha.on("mouseup", onUp);
                scope.photoAlpha.on("touchend", onUp);
                scope.photoAlpha.on("mouseupoutside", onUp);
                scope.photoAlpha.on("touchendoutside", onUp);
            }

            function onUp(e) {
                scope.photoAlpha.off("mousemove", onMove);
                scope.photoAlpha.off("touchmove", onMove);
                scope.photoAlpha.off("mouseup", onUp);
                scope.photoAlpha.off("touchend", onUp);
                scope.photoAlpha.off("mouseupoutside", onUp);
                scope.photoAlpha.off("touchendoutside", onUp);
            }

            function onMove(e) {
                var localPos = e.data.getLocalPosition(scope.photoContainer);
                var changedPos = {
                    x: localPos.x - startPos.x,
                    y: localPos.y - startPos.y
                }

                scope.photoAlpha.x = startPhotoPos.x + changedPos.x;
                scope.photoAlpha.y = startPhotoPos.y + changedPos.y;

                scope.checkPhotoPosition();
            };

            if (params != null) {
                if (params && typeof params.orientation != "undefined") {
                    scope.photoContainer.rotation = GMath.degToRad(-params.orientation);
                    //scope.photo.rotation = GMath.degToRad(-params.orientation);
                    //scope.photoAlpha.rotation = scope.photo.rotation;
                }
            }

            if (callback) callback();

        }
    },

    onRuntime: function () {
        var scope = this;
        //if(scope.img) scope.img.rotation += 0.03;
        //if(scope.shape) scope.shape.rotation += 0.02;
        //if(scope.shape) scope.shape.scale.x += 0.005;
        //if(scope.shape) scope.shape.scale.y += 0.005;

        if (scope.checkCanvasSizeChange) {
            scope.checkCanvasSizeChange();
        }
    },

    checkCanvasSizeChange: function () {
        var scope = PIXIApp;

        if (scope.autoSize == "flexible") {
            scope.canvasOldSize = { width: scope.canvas.width, height: scope.canvas.height };

            if (Math.round($("." + scope.canvasHolderClass).outerWidth()) != scope.canvasSize.width) {
                scope.canvasSize.width = Math.round($("." + scope.canvasHolderClass).outerWidth());
                scope.canvas.width = Math.round($("." + scope.canvasHolderClass).outerWidth());

                scope.onCanvasSizeChange();
            }

            if (Math.round($("." + scope.canvasHolderClass).height()) != scope.canvasSize.height) {
                scope.canvasSize.height = Math.round($("." + scope.canvasHolderClass).height());
                scope.canvas.height = $("." + scope.canvasHolderClass).height();

                scope.onCanvasSizeChange();
            }
        }
        else // autoSize = "fixsize"
        {
            if (Math.round($("." + scope.canvasHolderClass).width()) != Math.round(scope.canvasSize.width * scope.globalScale.x)) {
                scope.globalScale.x = Math.round($("." + scope.canvasHolderClass).width()) / scope.canvasSize.width;
                //console.log(scope.globalScale.x);
                TweenMax.set($("." + scope.canvasHolderClass + " canvas"), { scaleX: scope.globalScale.x });
                scope.onCanvasSizeChange();
            }
            //console.log($("." + scope.canvasHolderClass).height(), scope.canvasSize.height * scope.globalScale.y)
            if (Math.round($("." + scope.canvasHolderClass).height()) != Math.round(scope.canvasSize.height * scope.globalScale.y)) {
                scope.globalScale.y = Math.round($("." + scope.canvasHolderClass).height()) / scope.canvasSize.height;
                //console.log(scope.globalScale.y);
                TweenMax.set($("." + scope.canvasHolderClass + " canvas"), { scaleY: scope.globalScale.y });
                scope.onCanvasSizeChange();
            }
        }
    },

    onCanvasSizeChange: function () {
        //console.log("Canvas size change");
        var scope = PIXIApp;

        var sw = scope.canvas.width;
        var sh = scope.canvas.height;

        scope.center = { x: scope.canvas.width / 2, y: scope.canvas.height / 2 };

        if (scope.isInit) {
            // resize code here...

            if (scope.photoContainer) {
                scope.photoContainer.x = scope.center.x;
                scope.photoContainer.y = scope.center.y;
            }
            // -- finish resize --
        }

        if (scope.renderer) {
            scope.renderer.resize(scope.canvas.width, scope.canvas.height);
        }
    }

}
pUpload.init();