/*
 List of plugins:
  - GScript 1.0
	- GDevice 1.0
  - GMath 1.0
  - GArray 1.0
  - GLayoutCSS 1.0
  - GUpload 1.0
*/

/* GMouse - version 1.0
Author: Goon Nguyen
================================================== */

var GMouse = {
    getGlobal: function (event) {
        var globalMouse = {
            x: event.pageX || event.originalEvent.changedTouches[0].pageX,
            y: event.pageY || event.originalEvent.changedTouches[0].pageY
        }
        return globalMouse;
    },

    getLocal: function (event, dom) {
        var offset = dom.offset();
        var globalMouse = GMouse.getGlobal(event);

        var localMouse = {
            x: globalMouse.x - offset.left,
            y: globalMouse.y - offset.top
        }

        return localMouse;
    },

    insideDom: function (dom) {

    }
}

/* GScript - version 1.0
Author: Goon Nguyen
================================================== */

if (!GScript) {
    var GScript = {
        load: function (url, callback) {
            var done = false;
            var result = { status: false, message: "" };
            var script = document.createElement('script');
            script.setAttribute('src', url);

            script.onload = handleLoad;
            script.onreadystatechange = handleReadyStateChange;
            script.onerror = handleError;

            document.head.appendChild(script);

            function handleLoad() {
                if (!done) {
                    done = true;

                    result.status = true;
                    result.message = "Script was loaded successfully";

                    if (callback) callback(result);
                }
            }

            function handleReadyStateChange() {
                var state;

                if (!done) {
                    state = script.readyState;
                    if (state === "complete") {
                        handleLoad();
                    }
                }
            }

            function handleError() {
                if (!done) {
                    done = true;
                    result.status = false;
                    result.message = "Failed to load script."
                    if (callback) callback(result);
                }
            }
        },

        unload: function (url, callback) {
            var scripts = document.getElementsByTagName("script");
            var result = { status: false, message: "" };

            for (var i = 0; i < scripts.length; i++) {
                var script = scripts[i];
                if (script.src) {
                    var src = script.src;
                    if (String(src).toLowerCase().indexOf(url.toLowerCase()) >= 0) {
                        script.parentElement.removeChild(script);
                        result.status = true;
                        result.message = "Unload script successfully.";
                    }
                }
            }

            if (!result.status) {
                result.message = "Script not found.";
            }

            if (callback) callback(result);

            return result;
        },

        isExisted: function (filename) {
            var scripts = document.getElementsByTagName("script");
            var existed = false;
            for (var i = 0; i < scripts.length; i++) {
                if (scripts[i].src) {
                    var src = scripts[i].src;
                    if (String(src).toLowerCase().indexOf(filename.toLowerCase()) >= 0) {
                        existed = true;
                    }
                    console.log(i, scripts[i].src)
                } else {
                    console.log(i, scripts[i].innerHTML)
                }
            }
            return existed;
        },

        loadList: function (array, callback) {
            var result = { status: false, message: "" };
            var count = 0;
            var total = array.length;
            //console.log("loadList")
            this.load(array[count], onComplete);

            function onComplete(result) {
                count++;
                //console.log(count, total)
                if (count == total) {
                    result.status = true;
                    result.message = "All scripts were loaded.";
                    if (callback) callback(result);
                } else {
                    GScript.load(array[count], onComplete);
                }
            }
        }
    }
}

/* GDevice - version 1.0
Author: Goon Nguyen
================================================== */

var GDevice = {
    tmpOri: "portrait", //landscape
    ratio: 16 / 9,
    tmpType: "mobile",
    get type() {
        GDevice.resize();
        return GDevice.tmpType;
    },

    get orientation() {
        GDevice.resize();
        return GDevice.tmpOri;
    },

    get width() {
        return $(window).width();
    },

    get height() {
        return $(window).height();
    },

    init: function () {
        $(window).resize(GDevice.resize);
        GDevice.resize();
    },
    resize: function (e) {
        var sw = $(window).width();
        var sh = $(window).height();

        GDevice.ratio = sw / sh;

        if (GDevice.ratio > 1) {
            GDevice.tmpOri = "landscape"

            if (sw > 1024) {
                GDevice.tmpType = "desktop"
            } else {
                if (sw > 640) {
                    GDevice.tmpType = "tablet"
                } else {
                    GDevice.tmpType = "mobile"
                }
            }

        } else if (GDevice.ratio < 1) {
            GDevice.tmpOri = "portrail"

            //console.log("sw: " + sw);
            if (sw > 770) {
                GDevice.tmpType = "desktop"
            } else {
                if (sw > 480) {
                    GDevice.tmpType = "tablet"
                } else {
                    GDevice.tmpType = "mobile"
                }
            }
        } else {
            GDevice.tmpOri = "square"
            GDevice.tmpType = "desktop"
        }

        //console.log(GDevice);
    }
}
$(document).ready(function () {
    GDevice.init();
})

/* GMath - version 1.0
Author: Goon Nguyen
================================================== */

var GMath = {
    random: function (number) {
        return number * Math.random();
    },
    randomInt: function (number) {
        return Math.floor(GMath.random(number));
    },
    randomPlusMinus: function (number) {
        return number * (Math.random() - Math.random());
    },
    randomIntPlusMinus: function (number) {
        return Math.round(GMath.randomPlusMinus(number));
    },
    randomFromTo: function (from, to) {
        return from + (to - from) * Math.random();
    },
    randomIntFromTo: function (from, to) {
        return Math.floor(GMath.randomFromTo(from, to));
    },

    angleRadBetween2Points: function (p1, p2) {
        return Math.atan2(p2.y - p1.y, p2.x - p1.x);
    },

    angleDegBetween2Points: function (p1, p2) {
        return GMath.radToDeg(GMath.angleRadBetween2Points(p1, p2));
    },

    degToRad: function (deg) {
        return deg * Math.PI / 180;
    },

    radToDeg: function (rad) {
        return rad * 180 / Math.PI;
    },

    angleRadBetween3Points: function (A, B, C) {
        var AB = Math.sqrt(Math.pow(B.x - A.x, 2) + Math.pow(B.y - A.y, 2));
        var BC = Math.sqrt(Math.pow(B.x - C.x, 2) + Math.pow(B.y - C.y, 2));
        var AC = Math.sqrt(Math.pow(C.x - A.x, 2) + Math.pow(C.y - A.y, 2));
        return Math.acos((BC * BC + AB * AB - AC * AC) / (2 * BC * AB));
    },

    getPointWithAngleAndRadius: function (angle, radius) {
        var p = { x: 0, y: 0 };
        p.x = radius * Math.cos(angle);
        p.y = radius * Math.sin(angle);
        return p;
    },

    distanceBetweenPoints: function (p1, p2) {
        var x1 = p1.x;
        var y1 = p1.y;

        var x2 = p2.x;
        var y2 = p2.y;

        var d = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

        return d;
    }
}

/* GArray - version 1.0
Author: Goon Nguyen
================================================== */

var GArray = {
    remove: function (item, array) {
        var index = array.indexOf(item);
        if (index > -1) {
            array.splice(index, 1);
        }
        return array;
    }
}

/* GLayoutCSS - version 1.0
Author: Goon Nguyen
================================================== */

var GLayoutCSS = {
    init: function () {
        console.log("[GLayoutCSS 1.0] Initialized!");
        $(window).resize(GLayoutCSS.resize);
        GLayoutCSS.resize();
    },
    resize: function (e) {
        var sw = $(window).width();
        var sh = $(window).height();
        if ($('.helper-layout-fullscreen').length > 0) {
            $('.helper-layout-fullscreen').width(sw);
            $('.helper-layout-fullscreen').height(sh);
        }
    }
}
$(function () {
    GLayoutCSS.init();
})

/* GUpload - version 1.0
Author: Goon Nguyen
================================================== */

var GUpload = {
    inputElement: null,
    customClass: "",
    customPostName: "photo",
    customUploadType: "images/*",
    reader: null,
    file: null,
    fileMetaData: {},

    onSelect: null, // (base64)
    onCancel: null,
    onStart: null,
    checkSelectedInt: null,

    browse: function (callback) {
        this.onSelect = callback;

        GUpload.resultBase64 = "";
        GUpload.file = null;
        GUpload.fileMetaData = {};
        this.inputElement = null;

        if ($(".gupload-input").length > 0) {
            $(".gupload-input").remove();
            console.log("input is existed");
        }

        $("body").append('<input class="gupload-input helper-hide ' + GUpload.customClass + '" type="file" name="' + GUpload.customPostName + '" accept="' + GUpload.customUploadType + '">');

        this.inputElement = $(".gupload-input");

        this.inputElement.on("change", onChangeHandler);
        $(".gupload-input")[0].onChange = onChangeHandler;
        //$(".gupload-input")[0].addEventListener("change", onChangeHandler);

        this.inputElement.click();

        /*$(window).focusout(function(){
            console.log("This input field has lost its focus.");
        });*/

        $(window).focusin(function () {
            //console.log("This input field has gained its focus.");
            clearTimeout(GUpload.checkSelectedInt)
            GUpload.checkSelectedInt = setTimeout(checkSelected, 500);
        });

        function checkSelected() {
            if (!GUpload.file) {
                console.log("[GUpload] onCancel");
                $(".gupload-input").remove();
                if (GUpload.onCancel) GUpload.onCancel();

                $(window).off("focusin");

                GUpload.file = null;
            } else {
                //
            }
        }

        //this.inputElement.focusin();

        //--

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

        function onChangeHandler() {
            //console.log($(this).val());
            if ($(this)[0].files[0]) {
                if (GUpload.onStart) GUpload.onStart();

                function guid() {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                            .toString(16)
                            .substring(1);
                    }
                    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
                }

                var formDataToUpload = new FormData();
                formDataToUpload.append("file", $(this)[0].files[0], guid() + '.jpg');
                //formDataToUpload.append("_token", $('[name="_token"]').attr('content'));

                $.ajax({
                    url:'/Home/ResizeImage',
                    type: 'POST',
                    method: 'POST',
                    data: formDataToUpload,
                    cache: false,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (data) {

                        var block = data.data.split(";");
                        var contentType = block[0].split(":")[1];
                        var realData = block[1].split(",")[1];

                        // GUpload.file = $(this)[0].files[0];
                        GUpload.file = b64toBlob(realData, contentType);
                        GUpload.fileMetaData = {
                            type: GUpload.file.type,
                            size: GUpload.file.size,
                            name: GUpload.file.name
                        };

                        if (window.FileReader && window.FileReader.prototype.readAsArrayBuffer) {
                            GUpload.reader = new FileReader();

                            GUpload.reader.addEventListener("load", GUpload.onReadDataURL);

                            if (GUpload.file) {
                                GUpload.reader.readAsDataURL(GUpload.file);
                            }
                        } else {
                            $(".gupload-input").remove();
                            alert("Please upload your browser to use this feature.");
                            throw "This browser is too old to use this feature.";
                        }
                    }, error: function (e) {
                        alert("Đã xảy ra lỗi ! Xin vui lòng nhấn F5 và thử lại.");
                        //window.location.reload();
                    }
                });
            } else {
                console.log("[GUpload] onCancel");
                GUpload.file = null;
                GUpload.resultBase64 = "";
                $(".gupload-input").remove();
                if (GUpload.onCancel) GUpload.onCancel();
            }
        } //--
    },

    onReadDataURL: function (e) {
        GUpload.resultBase64 = GUpload.reader.result;

        //GUpload.reader.removeEventListener("load", GUpload.onReadDataURL);

        var reader = new FileReader();
        //GUpload.reader.onload = GUpload.onReadBuffer;
        if (reader.readAsArrayBuffer) {
            reader.addEventListener("load", GUpload.onReadBuffer);
            reader.readAsArrayBuffer(GUpload.file.slice(0, 64 * 1024));
        } else {
            console.log("[GUpload] onSelect");
            if (GUpload.onSelect) GUpload.onSelect(GUpload.resultBase64, null);
            $(".gupload-input").remove();
            GUpload.file = null;
        }
    },

    onReadBuffer: function (e) {
        var orientation = GUpload.getOrientation(e.target.result);
        var params = {}
        switch (orientation) {
            case 6:
                params.orientation = -90;
                break;
            case 8:
                params.orientation = 90;
                break;
            case 3:
                params.orientation = 180;
                break;
            default:
                params.orientation = 0;
                break;
        }

        $(".gupload-input").remove();

        console.log("[GUpload] onSelect");
        if (GUpload.onSelect) GUpload.onSelect(GUpload.resultBase64, params);

        GUpload.file = null;
    },

    getOrientation: function (dataBuffer) {
        var view = new DataView(dataBuffer);
        if (view.getUint16(0, false) != 0xFFD8) return -2;
        var length = view.byteLength,
            offset = 2;
        while (offset < length) {
            var marker = view.getUint16(offset, false);
            offset += 2;
            if (marker == 0xFFE1) {
                if (view.getUint32(offset += 2, false) != 0x45786966) return -1;
                var little = view.getUint16(offset += 6, false) == 0x4949;
                offset += view.getUint32(offset + 4, little);
                var tags = view.getUint16(offset, little);
                offset += 2;
                for (var i = 0; i < tags; i++)
                    if (view.getUint16(offset + (i * 12), little) == 0x0112)
                        return view.getUint16(offset + (i * 12) + 8, little);
            } else if ((marker & 0xFF00) != 0xFF00) break;
            else offset += view.getUint16(offset, false);
        }
        return -1;
    },

    onRead: function () { },

    onFailRead: function () {

    }
}
