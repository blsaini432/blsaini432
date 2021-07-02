
try {
    "undefined" == typeof jQuery.migrateMute && (jQuery.migrateMute = !0),
        function(a, b, c) {
            function d(c) {
                var d = b.console;
                f[c] || (f[c] = !0, a.migrateWarnings.push(c), d && d.warn && !a.migrateMute && (d.warn("JQMIGRATE: " + c), a.migrateTrace && d.trace && d.trace()))
            }

            function e(b, c, e, f) {
                if (Object.defineProperty) try {
                    return void Object.defineProperty(b, c, {
                        configurable: !0,
                        enumerable: !0,
                        get: function() {
                            return d(f), e
                        },
                        set: function(a) {
                            d(f), e = a
                        }
                    })
                } catch (g) {}
                a._definePropertyBroken = !0, b[c] = e
            }
            a.migrateVersion = "1.4.1";
            var f = {};
            a.migrateWarnings = [], b.console && b.console.log && b.console.log("JQMIGRATE: Migrate is installed" + (a.migrateMute ? "" : " with logging active") + ", version " + a.migrateVersion), a.migrateTrace === c && (a.migrateTrace = !0), a.migrateReset = function() {
                f = {}, a.migrateWarnings.length = 0
            }, "BackCompat" === document.compatMode && d("jQuery is not compatible with Quirks Mode");
            var g = a("<input/>", {
                    size: 1
                }).attr("size") && a.attrFn,
                h = a.attr,
                i = a.attrHooks.value && a.attrHooks.value.get || function() {
                    return null
                },
                j = a.attrHooks.value && a.attrHooks.value.set || function() {
                    return c
                },
                k = /^(?:input|button)$/i,
                l = /^[238]$/,
                m = /^(?:autofocus|autoplay|async|checked|controls|defer|disabled|hidden|loop|multiple|open|readonly|required|scoped|selected)$/i,
                n = /^(?:checked|selected)$/i;
            e(a, "attrFn", g || {}, "jQuery.attrFn is deprecated"), a.attr = function(b, e, f, i) {
                var j = e.toLowerCase(),
                    o = b && b.nodeType;
                return i && (h.length < 4 && d("jQuery.fn.attr( props, pass ) is deprecated"), b && !l.test(o) && (g ? e in g : a.isFunction(a.fn[e]))) ? a(b)[e](f) : ("type" === e && f !== c && k.test(b.nodeName) && b.parentNode && d("Can't change the 'type' of an input or button in IE 6/7/8"), !a.attrHooks[j] && m.test(j) && (a.attrHooks[j] = {
                    get: function(b, d) {
                        var e, f = a.prop(b, d);
                        return f === !0 || "boolean" != typeof f && (e = b.getAttributeNode(d)) && e.nodeValue !== !1 ? d.toLowerCase() : c
                    },
                    set: function(b, c, d) {
                        var e;
                        return c === !1 ? a.removeAttr(b, d) : (e = a.propFix[d] || d, e in b && (b[e] = !0), b.setAttribute(d, d.toLowerCase())), d
                    }
                }, n.test(j) && d("jQuery.fn.attr('" + j + "') might use property instead of attribute")), h.call(a, b, e, f))
            }, a.attrHooks.value = {
                get: function(a, b) {
                    var c = (a.nodeName || "").toLowerCase();
                    return "button" === c ? i.apply(this, arguments) : ("input" !== c && "option" !== c && d("jQuery.fn.attr('value') no longer gets properties"), b in a ? a.value : null)
                },
                set: function(a, b) {
                    var c = (a.nodeName || "").toLowerCase();

                    return "button" === c ? j.apply(this, arguments) : ("input" !== c && "option" !== c && d("jQuery.fn.attr('value', val) no longer sets properties"), void(a.value = b))
                }
            };
            var o, p, q = a.fn.init,
                r = a.find,
                s = a.parseJSON,
                t = /^\s*</,
                u = /\[(\s*[-\w]+\s*)([~|^$*]?=)\s*([-\w#]*?#[-\w#]*)\s*\]/,
                v = /\[(\s*[-\w]+\s*)([~|^$*]?=)\s*([-\w#]*?#[-\w#]*)\s*\]/g,
                w = /^([^<]*)(<[\w\W]+>)([^>]*)$/;
            a.fn.init = function(b, e, f) {
                var g, h;
                return b && "string" == typeof b && !a.isPlainObject(e) && (g = w.exec(a.trim(b))) && g[0] && (t.test(b) || d("$(html) HTML strings must start with '<' character"), g[3] && d("$(html) HTML text after last tag is ignored"), "#" === g[0].charAt(0) && (d("HTML string cannot start with a '#' character"), a.error("JQMIGRATE: Invalid selector string (XSS)")), e && e.context && e.context.nodeType && (e = e.context), a.parseHTML) ? q.call(this, a.parseHTML(g[2], e && e.ownerDocument || e || document, !0), e, f) : (h = q.apply(this, arguments), b && b.selector !== c ? (h.selector = b.selector, h.context = b.context) : (h.selector = "string" == typeof b ? b : "", b && (h.context = b.nodeType ? b : e || document)), h)
            }, a.fn.init.prototype = a.fn, a.find = function(a) {
                var b = Array.prototype.slice.call(arguments);
                if ("string" == typeof a && u.test(a)) try {
                    document.querySelector(a)
                } catch (c) {
                    a = a.replace(v, function(a, b, c, d) {
                        return "[" + b + c + '"' + d + '"]'
                    });
                    try {
                        document.querySelector(a), d("Attribute selector with '#' must be quoted: " + b[0]), b[0] = a
                    } catch (e) {
                        d("Attribute selector with '#' was not fixed: " + b[0])
                    }
                }
                return r.apply(this, b)
            };
            var x;
            for (x in r) Object.prototype.hasOwnProperty.call(r, x) && (a.find[x] = r[x]);
            a.parseJSON = function(a) {
                return a ? s.apply(this, arguments) : (d("jQuery.parseJSON requires a valid JSON string"), null)
            }, a.uaMatch = function(a) {
                a = a.toLowerCase();
                var b = /(chrome)[ \/]([\w.]+)/.exec(a) || /(webkit)[ \/]([\w.]+)/.exec(a) || /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(a) || /(msie) ([\w.]+)/.exec(a) || a.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(a) || [];
                return {
                    browser: b[1] || "",
                    version: b[2] || "0"
                }
            }, a.browser || (o = a.uaMatch(navigator.userAgent), p = {}, o.browser && (p[o.browser] = !0, p.version = o.version), p.chrome ? p.webkit = !0 : p.webkit && (p.safari = !0), a.browser = p), e(a, "browser", a.browser, "jQuery.browser is deprecated"), a.boxModel = a.support.boxModel = "CSS1Compat" === document.compatMode, e(a, "boxModel", a.boxModel, "jQuery.boxModel is deprecated"), e(a.support, "boxModel", a.support.boxModel, "jQuery.support.boxModel is deprecated"), a.sub = function() {
                function b(a, c) {
                    return new b.fn.init(a, c)
                }
                a.extend(!0, b, this), b.superclass = this, b.fn = b.prototype = this(), b.fn.constructor = b, b.sub = this.sub, b.fn.init = function(d, e) {
                    var f = a.fn.init.call(this, d, e, c);
                    return f instanceof b ? f : b(f)
                }, b.fn.init.prototype = b.fn;
                var c = b(document);
                return d("jQuery.sub() is deprecated"), b
            }, a.fn.size = function() {
                return d("jQuery.fn.size() is deprecated; use the .length property"), this.length
            };
            var y = !1;
            a.swap && a.each(["height", "width", "reliableMarginRight"], function(b, c) {
                var d = a.cssHooks[c] && a.cssHooks[c].get;
                d && (a.cssHooks[c].get = function() {
                    var a;
                    return y = !0, a = d.apply(this, arguments), y = !1, a
                })
            }), a.swap = function(a, b, c, e) {
                var f, g, h = {};
                y || d("jQuery.swap() is undocumented and deprecated");
                for (g in b) h[g] = a.style[g], a.style[g] = b[g];
                f = c.apply(a, e || []);
                for (g in b) a.style[g] = h[g];
                return f
            }, a.ajaxSetup({
                converters: {
                    "text json": a.parseJSON
                }
            });
            var B = a.event.add,
                C = a.event.remove,
                D = a.event.trigger,
                E = a.fn.toggle,
                F = a.fn.live,
                G = a.fn.die,
                H = a.fn.load,
                I = "ajaxStart|ajaxStop|ajaxSend|ajaxComplete|ajaxError|ajaxSuccess",
                J = new RegExp("\\b(?:" + I + ")\\b"),
                K = /(?:^|\s)hover(\.\S+|)\b/,
                L = function(b) {
                    return "string" != typeof b || a.event.special.hover ? b : (K.test(b) && d("'hover' pseudo-event is deprecated, use 'mouseenter mouseleave'"), b && b.replace(K, "mouseenter$1 mouseleave$1"))
                };
            a.event.props && "attrChange" !== a.event.props[0] && a.event.props.unshift("attrChange", "attrName", "relatedNode", "srcElement"), a.event.dispatch && e(a.event, "handle", a.event.dispatch, "jQuery.event.handle is undocumented and deprecated"), a.event.add = function(a, b, c, e, f) {
                a !== document && J.test(b) && d("AJAX events should be attached to document: " + b), B.call(this, a, L(b || ""), c, e, f)
            }, a.event.remove = function(a, b, c, d, e) {
                C.call(this, a, L(b) || "", c, d, e)
            }, a.each(["load", "unload", "error"], function(b, c) {
                a.fn[c] = function() {
                    var a = Array.prototype.slice.call(arguments, 0);
                    return "load" === c && "string" == typeof a[0] ? H.apply(this, a) : (d("jQuery.fn." + c + "() is deprecated"), a.splice(0, 0, c), arguments.length ? this.bind.apply(this, a) : (this.triggerHandler.apply(this, a), this))
                }
            }), a.fn.toggle = function(b, c) {
                if (!a.isFunction(b) || !a.isFunction(c)) return E.apply(this, arguments);
                d("jQuery.fn.toggle(handler, handler...) is deprecated");
                var e = arguments,
                    f = b.guid || a.guid++,
                    g = 0,
                    h = function(c) {
                        var d = (a._data(this, "lastToggle" + b.guid) || 0) % g;
                        return a._data(this, "lastToggle" + b.guid, d + 1), c.preventDefault(), e[d].apply(this, arguments) || !1
                    };
                for (h.guid = f; g < e.length;) e[g++].guid = f;
                return this.click(h)
            }, a.fn.live = function(b, c, e) {
                return d("jQuery.fn.live() is deprecated"), F ? F.apply(this, arguments) : (a(this.context).on(b, this.selector, c, e), this)
            }, a.fn.die = function(b, c) {
                return d("jQuery.fn.die() is deprecated"), G ? G.apply(this, arguments) : (a(this.context).off(b, this.selector || "**", c), this)
            }, a.event.trigger = function(a, b, c, e) {
                return c || J.test(a) || d("Global events are undocumented and deprecated"), D.call(this, a, b, c || document, e)
            }, a.each(I.split("|"), function(b, c) {
                a.event.special[c] = {
                    setup: function() {
                        var b = this;
                        return b !== document && (a.event.add(document, c + "." + a.guid, function() {
                            a.event.trigger(c, Array.prototype.slice.call(arguments, 1), b, !0)
                        }), a._data(this, c, a.guid++)), !1
                    },
                    teardown: function() {
                        return this !== document && a.event.remove(document, c + "." + a._data(this, c)), !1
                    }
                }
            }), a.event.special.ready = {
                setup: function() {
                    this === document && d("'ready' event is deprecated")
                }
            };
            var M = a.fn.andSelf || a.fn.addBack,
                N = a.fn.find;
            if (a.fn.andSelf = function() {
                    return d("jQuery.fn.andSelf() replaced by jQuery.fn.addBack()"), M.apply(this, arguments)
                }, a.fn.find = function(a) {
                    var b = N.apply(this, arguments);
                    return b.context = this.context, b.selector = this.selector ? this.selector + " " + a : a, b
                }, a.Callbacks) {
                var O = a.Deferred,
                    P = [
                        ["resolve", "done", a.Callbacks("once memory"), a.Callbacks("once memory"), "resolved"],
                        ["reject", "fail", a.Callbacks("once memory"), a.Callbacks("once memory"), "rejected"],
                        ["notify", "progress", a.Callbacks("memory"), a.Callbacks("memory")]
                    ];
                a.Deferred = function(b) {
                    var c = O(),
                        e = c.promise();
                    return c.pipe = e.pipe = function() {
                        var b = arguments;
                        return d("deferred.pipe() is deprecated"), a.Deferred(function(d) {
                            a.each(P, function(f, g) {
                                var h = a.isFunction(b[f]) && b[f];
                                c[g[1]](function() {
                                    var b = h && h.apply(this, arguments);
                                    b && a.isFunction(b.promise) ? b.promise().done(d.resolve).fail(d.reject).progress(d.notify) : d[g[0] + "With"](this === e ? d.promise() : this, h ? [b] : arguments)
                                })
                            }), b = null
                        }).promise()
                    }, c.isResolved = function() {
                        return d("deferred.isResolved is deprecated"), "resolved" === c.state()
                    }, c.isRejected = function() {
                        return d("deferred.isRejected is deprecated"), "rejected" === c.state()
                    }, b && b.call(c, c), c
                }
            }
        }(jQuery, window);
} catch (e) {
    console.error('Error in file:/quadra/media/jui/js/jquery-migrate.min.js?373bd73b08e83b2834fe00cf72c018e6; Error:' + e.message);
};

try {
    ! function(a) {
        "use strict";
        "function" == typeof define && define.amd ? define(["jquery"], a) : "undefined" != typeof exports ? module.exports = a(require("jquery")) : a(jQuery)
    }(function(a) {
        "use strict";
        var b = window.Slick || {};
        b = function() {
            function c(c, d) {
                var f, e = this;
                     e.defaults = {
                     touchObject: {},
                    transformsEnabled: !1,
                    unslicked: !1
                }, a.extend(e, e.initials), e.activeBreakpoint = null, e.animType = null, e.animProp = null, e.breakpoints = [], e.breakpointSettings = [], e.cssTransitions = !1, e.focussed = !1, e.interrupted = !1, e.hidden = "hidden", e.paused = !0, e.positionProp = null, e.respondTo = null, e.rowCount = 1, e.shouldClick = !0, e.$slider = a(c), e.$slidesCache = null, e.transformType = null, e.transitionType = null, e.visibilityChange = "visibilitychange", e.windowWidth = 0, e.windowTimer = null, f = a(c).data("slick") || {}, e.options = a.extend({}, e.defaults, d, f), e.currentSlide = e.options.initialSlide, e.originalSettings = e.options, "undefined" != typeof document.mozHidden ? (e.hidden = "mozHidden", e.visibilityChange = "mozvisibilitychange") : "undefined" != typeof document.webkitHidden && (e.hidden = "webkitHidden", e.visibilityChange = "webkitvisibilitychange"), e.autoPlay = a.proxy(e.autoPlay, e), e.autoPlayClear = a.proxy(e.autoPlayClear, e), e.autoPlayIterator = a.proxy(e.autoPlayIterator, e), e.changeSlide = a.proxy(e.changeSlide, e), e.clickHandler = a.proxy(e.clickHandler, e), e.selectHandler = a.proxy(e.selectHandler, e), e.setPosition = a.proxy(e.setPosition, e), e.swipeHandler = a.proxy(e.swipeHandler, e), e.dragHandler = a.proxy(e.dragHandler, e), e.keyHandler = a.proxy(e.keyHandler, e), e.instanceUid = b++, e.htmlExpr = /^(?:\s*(<[\w\W]+>)[^>]*)$/, e.registerBreakpoints(), e.init(!0)
            }
            var b = 0;
            return c
        }(), b.prototype.activateADA = function() {
            var a = this;
            a.$slideTrack.find(".slick-active").attr({
                "aria-hidden": "false"
            }).find("a, input, button, select").attr({
                tabindex: "0"
            })
         }, b.prototype.checkResponsive = function(b, c) {
            var e, f, g, d = this,
                h = !1,
                i = d.$slider.width(),
                j = window.innerWidth || a(window).width();
            if ("window" === d.respondTo ? g = j : "slider" === d.respondTo ? g = i : "min" === d.respondTo && (g = Math.min(j, i)), d.options.responsive && d.options.responsive.length && null !== d.options.responsive) {
                f = null;
                for (e in d.breakpoints) d.breakpoints.hasOwnProperty(e) && (d.originalSettings.mobileFirst === !1 ? g < d.breakpoints[e] && (f = d.breakpoints[e]) : g > d.breakpoints[e] && (f = d.breakpoints[e]));
                null !== f ? null !== d.activeBreakpoint ? (f !== d.activeBreakpoint || c) && (d.activeBreakpoint = f, "unslick" === d.breakpointSettings[f] ? d.unslick(f) : (d.options = a.extend({}, d.originalSettings, d.breakpointSettings[f]), b === !0 && (d.currentSlide = d.options.initialSlide), d.refresh(b)), h = f) : (d.activeBreakpoint = f, "unslick" === d.breakpointSettings[f] ? d.unslick(f) : (d.options = a.extend({}, d.originalSettings, d.breakpointSettings[f]), b === !0 && (d.currentSlide = d.options.initialSlide), d.refresh(b)), h = f) : null !== d.activeBreakpoint && (d.activeBreakpoint = null, d.options = d.originalSettings, b === !0 && (d.currentSlide = d.options.initialSlide), d.refresh(b), h = f), b || h === !1 || d.$slider.trigger("breakpoint", [d, h])
            }
        }, b.prototype.changeSlide = function(b, c) {
            var f, g, h, d = this,
                e = a(b.currentTarget);
            switch (e.is("a") && b.preventDefault(), e.is("li") || (e = e.closest("li")), h = d.slideCount % d.options.slidesToScroll !== 0, f = h ? 0 : (d.slideCount - d.currentSlide) % d.options.slidesToScroll, b.data.message) {
                case "previous":
                    g = 0 === f ? d.options.slidesToScroll : d.options.slidesToShow - f, d.slideCount > d.options.slidesToShow && d.slideHandler(d.currentSlide - g, !1, c);
                    break;
                case "next":
                    g = 0 === f ? d.options.slidesToScroll : f, d.slideCount > d.options.slidesToShow && d.slideHandler(d.currentSlide + g, !1, c);
                    break;
                case "index":
                    var i = 0 === b.data.index ? 0 : b.data.index || e.index() * d.options.slidesToScroll;
                    d.slideHandler(d.checkNavigable(i), !1, c), e.children().trigger("focus");
                    break;
                default:
                    return
            }
        }, b.prototype.checkNavigable = function(a) {
            var c, d, b = this;
            if (c = b.getNavigableIndexes(), d = 0, a > c[c.length - 1]) a = c[c.length - 1];
            else
                for (var e in c) {
                    if (a < c[e]) {
                        a = d;
                        break
                    }
                    d = c[e]
                }
            return a
        }, b.prototype.getDotCount = function() {
            var a = this,
                b = 0,
                c = 0,
                d = 0;
            if (a.options.infinite === !0)
                for (; b < a.slideCount;) ++d, b = c + a.options.slidesToScroll, c += a.options.slidesToScroll <= a.options.slidesToShow ? a.options.slidesToScroll : a.options.slidesToShow;
            else if (a.options.centerMode === !0) d = a.slideCount;
            else if (a.options.asNavFor)
                for (; b < a.slideCount;) ++d, b = c + a.options.slidesToScroll, c += a.options.slidesToScroll <= a.options.slidesToShow ? a.options.slidesToScroll : a.options.slidesToShow;
            else d = 1 + Math.ceil((a.slideCount - a.options.slidesToShow) / a.options.slidesToScroll);
            return d - 1
        }, b.prototype.getLeft = function(a) {
            var c, d, f, b = this,
                e = 0;
            return b.slideOffset = 0, d = b.$slides.first().outerHeight(!0), b.options.infinite === !0 ? (b.slideCount > b.options.slidesToShow && (b.slideOffset = b.slideWidth * b.options.slidesToShow * -1, e = d * b.options.slidesToShow * -1), b.slideCount % b.options.slidesToScroll !== 0 && a + b.options.slidesToScroll > b.slideCount && b.slideCount > b.options.slidesToShow && (a > b.slideCount ? (b.slideOffset = (b.options.slidesToShow - (a - b.slideCount)) * b.slideWidth * -1, e = (b.options.slidesToShow - (a - b.slideCount)) * d * -1) : (b.slideOffset = b.slideCount % b.options.slidesToScroll * b.slideWidth * -1, e = b.slideCount % b.options.slidesToScroll * d * -1))) : a + b.options.slidesToShow > b.slideCount && (b.slideOffset = (a + b.options.slidesToShow - b.slideCount) * b.slideWidth, e = (a + b.options.slidesToShow - b.slideCount) * d), b.slideCount <= b.options.slidesToShow && (b.slideOffset = 0, e = 0), b.options.centerMode === !0 && b.options.infinite === !0 ? b.slideOffset += b.slideWidth * Math.floor(b.options.slidesToShow / 2) - b.slideWidth : b.options.centerMode === !0 && (b.slideOffset = 0, b.slideOffset += b.slideWidth * Math.floor(b.options.slidesToShow / 2)), c = b.options.vertical === !1 ? a * b.slideWidth * -1 + b.slideOffset : a * d * -1 + e, b.options.variableWidth === !0 && (f = b.slideCount <= b.options.slidesToShow || b.options.infinite === !1 ? b.$slideTrack.children(".slick-slide").eq(a) : b.$slideTrack.children(".slick-slide").eq(a + b.options.slidesToShow), c = b.options.rtl === !0 ? f[0] ? -1 * (b.$slideTrack.width() - f[0].offsetLeft - f.width()) : 0 : f[0] ? -1 * f[0].offsetLeft : 0, b.options.centerMode === !0 && (f = b.slideCount <= b.options.slidesToShow || b.options.infinite === !1 ? b.$slideTrack.children(".slick-slide").eq(a) : b.$slideTrack.children(".slick-slide").eq(a + b.options.slidesToShow + 1), c = b.options.rtl === !0 ? f[0] ? -1 * (b.$slideTrack.width() - f[0].offsetLeft - f.width()) : 0 : f[0] ? -1 * f[0].offsetLeft : 0, c += (b.$list.width() - f.outerWidth()) / 2)), c
        }, b.prototype.getOption = b.prototype.slickGetOption = function(a) {
            var b = this;
            return b.options[a]
        }, b.prototype.getNavigableIndexes = function() {
            var e, a = this,
                b = 0,
                c = 0,
                d = [];
            for (a.options.infinite === !1 ? e = a.slideCount : (b = -1 * a.options.slidesToScroll, c = -1 * a.options.slidesToScroll, e = 2 * a.slideCount); e > b;) d.push(b), b = c + a.options.slidesToScroll, c += a.options.slidesToScroll <= a.options.slidesToShow ? a.options.slidesToScroll : a.options.slidesToShow;
            return d
          }, a.fn.slick = function() {
            var f, g, a = this,
                c = arguments[0],
                d = Array.prototype.slice.call(arguments, 1),
                e = a.length;
            for (f = 0; e > f; f++)
                if ("object" == typeof c || "undefined" == typeof c ? a[f].slick = new b(a[f], c) : g = a[f].slick[c].apply(a[f].slick, d), "undefined" != typeof g) return g;
            return a
        }
    });
} catch (e) {
    console.error('Error in file:/quadra/templates/quadra/js/slick.min.js; Error:' + e.message);
};
try {
    ! function() {
        "use strict";
        document.addEventListener("DOMContentLoaded", function() {
            var o = Joomla.getOptions("system.keepalive"),
                n = o && o.uri ? o.uri.replace(/&amp;/g, "&") : "",
                t = o && o.interval ? o.interval : 45e3;
            if ("" === n) {
                var e = Joomla.getOptions("system.paths");
                n = (e ? e.root + "/index.php" : window.location.pathname) + "?option=com_ajax&format=json"
            }
            window.setInterval(function() {
                Joomla.request({
                    url: n,
                    onSuccess: function() {},
                    onError: function() {}
                })
            }, t)
        })
    }(window, document, Joomla);
} catch (e) {
    console.error('Error in file:/quadra/media/system/js/keepalive.js?373bd73b08e83b2834fe00cf72c018e6; Error:' + e.message);
};
try {
    "function" !== typeof Object.create && (Object.create = function(f) {
        function g() {}
        g.prototype = f;
        return new g
    });
    (function(f, g, k) {
        var l = {
            init: function(a, b) {
                this.$elem = f(b);
                this.options = f.extend({}, f.fn.owlCarousel.options, this.$elem.data(), a);
                this.userOptions = a;
                this.loadContent()
            },
            response: function() {
                var a = this,
                    b, e;
                if (!0 !== a.options.responsive) return !1;
                e = f(g).width();
                a.resizer = function() {
                    f(g).width() !== e && (!1 !== a.options.autoPlay && g.clearInterval(a.autoPlayInterval), g.clearTimeout(b), b = g.setTimeout(function() {
                        e = f(g).width();
                        a.updateVars()
                    }, a.options.responsiveRefreshRate))
                };
                f(g).resize(a.resizer)
            },
            appendWrapperSizes: function() {
                this.$owlWrapper.css({
                    width: this.$owlItems.length * this.itemWidth * 2,
                    left: 0
                });
                this.appendItemsSizes()
            },
            buildButtons: function() {
                var a = this,
                    b = f('<div class="owl-buttons"/>');
                a.owlControls.append(b);
                a.buttonPrev = f("<div/>", {
                    "class": "owl-prev",
                    html: a.options.navigationText[0] || ""
                });
                b.on("touchend.owlControls mouseup.owlControls", 'div[class^="owl"]', function(b) {
                    b.preventDefault();
                    f(this).hasClass("owl-next") ? a.next() : a.prev()
                })
            },
            buildPagination: function() {
                var a = this;
                a.paginationWrapper = f('<div class="owl-pagination"/>');
                a.owlControls.append(a.paginationWrapper);
                a.paginationWrapper.on("touchend.owlControls mouseup.owlControls", ".owl-page", function(b) {
                    b.preventDefault();
                    Number(f(this).data("owl-page")) !== a.currentItem && a.goTo(Number(f(this).data("owl-page")), !0)
                })
            },
            updatePagination: function() {
                var a, b, e, c, d, g;
                if (!1 === this.options.pagination) return !1;
                this.paginationWrapper.html("");
                a = 0;
                b = this.itemsAmount - this.itemsAmount % this.options.items;
                for (c = 0; c < this.itemsAmount; c += 1) 0 === c % this.options.items && (a += 1, b === c && (e = this.itemsAmount - this.options.items), d = f("<div/>", {
                    "class": "owl-page"
                }), g = f("<span></span>", {
                    text: !0 === this.options.paginationNumbers ? a : "",
                    "class": !0 === this.options.paginationNumbers ? "owl-numbers" : ""
                }), d.append(g), d.data("owl-page", b === c ? e : c), d.data("owl-roundPages", a), this.paginationWrapper.append(d));
                this.checkPagination()
            },
            checkPagination: function() {
                var a = this;
                if (!1 === a.options.pagination) return !1;
                a.paginationWrapper.find(".owl-page").each(function() {
                    f(this).data("owl-roundPages") === f(a.$owlItems[a.currentItem]).data("owl-roundPages") && (a.paginationWrapper.find(".owl-page").removeClass("active"), f(this).addClass("active"))
                })
            },
            checkNavigation: function() {
                if (!1 === this.options.navigation) return !1;
                !1 === this.options.rewindNav && (0 === this.currentItem && 0 === this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.addClass("disabled")) : 0 === this.currentItem && 0 !== this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.removeClass("disabled")) : this.currentItem === this.maximumItem ? (this.buttonPrev.removeClass("disabled"), this.buttonNext.addClass("disabled")) : 0 !== this.currentItem && this.currentItem !== this.maximumItem && (this.buttonPrev.removeClass("disabled"), this.buttonNext.removeClass("disabled")))
            },
            updateControls: function() {
                this.updatePagination();
                this.checkNavigation();
                this.owlControls && (this.options.items >= this.itemsAmount ? this.owlControls.hide() : this.owlControls.show())
            },
            destroyControls: function() {
                this.owlControls && this.owlControls.remove()
            },
            next: function(a) {
                if (this.isTransition) return !1;
                this.currentItem += !0 === this.options.scrollPerPage ? this.options.items : 1;
                if (this.currentItem > this.maximumItem + (!0 === this.options.scrollPerPage ? this.options.items - 1 : 0))
                    if (!0 === this.options.rewindNav) this.currentItem = 0, a = "rewind";
                    else return this.currentItem = this.maximumItem, !1;
                this.goTo(this.currentItem, a)
            },
            prev: function(a) {
                if (this.isTransition) return !1;
                this.currentItem = !0 === this.options.scrollPerPage && 0 < this.currentItem && this.currentItem < this.options.items ? 0 : this.currentItem - (!0 === this.options.scrollPerPage ? this.options.items : 1);
                if (0 > this.currentItem)
                    if (!0 === this.options.rewindNav) this.currentItem = this.maximumItem, a = "rewind";
                    else return this.currentItem = 0, !1;
                this.goTo(this.currentItem, a)
            },
            jumpTo: function(a) {
                "function" === typeof this.options.beforeMove && this.options.beforeMove.apply(this, [this.$elem]);
                a >= this.maximumItem || -1 === a ? a = this.maximumItem : 0 >= a && (a = 0);
                this.swapSpeed(0);
                !0 === this.browser.support3d ? this.transition3d(this.positionsInArray[a]) : this.css2slide(this.positionsInArray[a], 1);
                this.currentItem = this.owl.currentItem = a;
                this.afterGo()
            },
            afterGo: function() {
                this.prevArr.push(this.currentItem);
                this.prevItem = this.owl.prevItem = this.prevArr[this.prevArr.length - 2];
                this.prevArr.shift(0);
                this.prevItem !== this.currentItem && (this.checkPagination(), this.checkNavigation(), this.eachMoveUpdate(), !1 !== this.options.autoPlay && this.checkAp());
                "function" === typeof this.options.afterMove && this.prevItem !== this.currentItem && this.options.afterMove.apply(this, [this.$elem])
            },
            stop: function() {
                this.apStatus = "stop";
                g.clearInterval(this.autoPlayInterval)
            },
            checkAp: function() {
                "stop" !== this.apStatus && this.play()
            },
            play: function() {
                var a = this;
                a.apStatus = "play";
                if (!1 === a.options.autoPlay) return !1;
                g.clearInterval(a.autoPlayInterval);
                a.autoPlayInterval = g.setInterval(function() {
                    a.next(!0)
                }, a.options.autoPlay)
            },
            swapSpeed: function(a) {
                "slideSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.slideSpeed)) : "paginationSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.paginationSpeed)) : "string" !== typeof a && this.$owlWrapper.css(this.addCssSpeed(a))
            },
            addCssSpeed: function(a) {
                return {
                    "-webkit-transition": "all " + a + "ms ease",
                    "-moz-transition": "all " + a + "ms ease",
                    "-o-transition": "all " + a + "ms ease",
                    transition: "all " + a + "ms ease"
                }
            },
            removeTransition: function() {
                return {
                    "-webkit-transition": "",
                    "-moz-transition": "",
                    "-o-transition": "",
                    transition: ""
                }
            },
            doTranslate: function(a) {
                return {
                    "-webkit-transform": "translate3d(" + a + "px, 0px, 0px)",
                    "-moz-transform": "translate3d(" + a + "px, 0px, 0px)",
                    "-o-transform": "translate3d(" + a + "px, 0px, 0px)",
                    "-ms-transform": "translate3d(" +
                        a + "px, 0px, 0px)",
                    transform: "translate3d(" + a + "px, 0px,0px)"
                }
            },
            eventTypes: function() {
                var a = ["s", "e", "x"];
                this.ev_types = {};
                !0 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl mousedown.owl", "touchmove.owl mousemove.owl", "touchend.owl touchcancel.owl mouseup.owl"] : !1 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl", "touchmove.owl", "touchend.owl touchcancel.owl"] : !0 === this.options.mouseDrag && !1 === this.options.touchDrag && (a = ["mousedown.owl", "mousemove.owl", "mouseup.owl"]);
                this.ev_types.start = a[0];
                this.ev_types.move = a[1];
                this.ev_types.end = a[2]
            },
            disabledEvents: function() {
                this.$elem.on("dragstart.owl", function(a) {
                    a.preventDefault()
                });
                this.$elem.on("mousedown.disableTextSelect", function(a) {
                    return f(a.target).is("input, textarea, select, option")
                })
            },
            gestures: function() {
                function a(a) {
              
                }

                function b(a) {
                    "on" === a ? (f(k).on(d.ev_types.move, e), f(k).on(d.ev_types.end, c)) : "off" === a && (f(k).off(d.ev_types.move), f(k).off(d.ev_types.end))
                }

                function e(b) {
                    b = b.originalEvent || b || g.event;
                    d.newPosX = a(b).x - h.offsetX;
                    d.newPosY = a(b).y - h.offsetY;
                    d.newRelativeX = d.newPosX - h.relativePos;
                    "function" === typeof d.options.startDragging && !0 !== h.dragging && 0 !== d.newRelativeX && (h.dragging = !0, d.options.startDragging.apply(d, [d.$elem]));
                    (8 < d.newRelativeX || -8 > d.newRelativeX) && !0 === d.browser.isTouch && (void 0 !== b.preventDefault ? b.preventDefault() : b.returnValue = !1, h.sliding = !0);
                    (10 < d.newPosY || -10 > d.newPosY) && !1 === h.sliding && f(k).off("touchmove.owl");
                    d.newPosX = Math.max(Math.min(d.newPosX, d.newRelativeX / 5), d.maximumPixels + d.newRelativeX / 5);
                    !0 === d.browser.support3d ? d.transition3d(d.newPosX) : d.css2move(d.newPosX)
                }

                function c(a) {
                    a = a.originalEvent || a || g.event;
                    var c;
                    a.target = a.target || a.srcElement;
                    h.dragging = !1;
                    !0 !== d.browser.isTouch && d.$owlWrapper.removeClass("grabbing");
                    d.dragDirection = 0 > d.newRelativeX ? d.owl.dragDirection = "left" : d.owl.dragDirection = "right";
                    0 !== d.newRelativeX && (c = d.getNewPosition(), d.goTo(c, !1, "drag"), h.targetElement === a.target && !0 !== d.browser.isTouch && (f(a.target).on("click.disable", function(a) {
                        a.stopImmediatePropagation();
                        a.stopPropagation();
                        a.preventDefault();
                        f(a.target).off("click.disable")
                    }), a = f._data(a.target, "events").click, c = a.pop(), a.splice(0, 0, c)));
                    b("off")
                }
                var d = this,
                    h = {
                        offsetX: 0,
                        offsetY: 0,
                        baseElWidth: 0,
                        relativePos: 0,
                        position: null,
                        minSwipe: null,
                        maxSwipe: null,
                        sliding: null,
                        dargging: null,
                        targetElement: null
                    };
                d.isCssFinish = !0;
                d.$elem.on(d.ev_types.start, ".owl-wrapper", function(c) {
                    c = c.originalEvent || c || g.event;
                    var e;
                    if (3 === c.which) return !1;
                    if (!(d.itemsAmount <= d.options.items)) {
                        if (!1 === d.isCssFinish && !d.options.dragBeforeAnimFinish || !1 === d.isCss3Finish && !d.options.dragBeforeAnimFinish) return !1;
                        !1 !== d.options.autoPlay && g.clearInterval(d.autoPlayInterval);
                        !0 === d.browser.isTouch || d.$owlWrapper.hasClass("grabbing") || d.$owlWrapper.addClass("grabbing");
                        d.newPosX = 0;
                        d.newRelativeX = 0;
                        f(this).css(d.removeTransition());
                        e = f(this).position();
                        h.relativePos = e.left;
                        h.offsetX = a(c).x - e.left;
                        h.offsetY = a(c).y - e.top;
                        b("on");
                        h.sliding = !1;
                        h.targetElement = c.target || c.srcElement
                    }
                })
            },
            getNewPosition: function() {
                var a = this.closestItem();
                a > this.maximumItem ? a = this.currentItem = this.maximumItem : 0 <= this.newPosX && (this.currentItem = a = 0);
                return a
            },
            closestItem: function() {
                var a = this,
                    b = !0 === a.options.scrollPerPage ? a.pagesInArray : a.positionsInArray,
                    e = a.newPosX,
                    c = null;
                f.each(b, function(d, g) {
                    e - a.itemWidth / 20 > b[d + 1] && e - a.itemWidth / 20 < g && "left" === a.moveDirection() ? (c = g, a.currentItem = !0 === a.options.scrollPerPage ? f.inArray(c, a.positionsInArray) : d) : e + a.itemWidth / 20 < g && e + a.itemWidth / 20 > (b[d + 1] || b[d] - a.itemWidth) && "right" === a.moveDirection() && (!0 === a.options.scrollPerPage ? (c = b[d + 1] || b[b.length - 1], a.currentItem = f.inArray(c, a.positionsInArray)) : (c = b[d + 1], a.currentItem = d + 1))
                });
                return a.currentItem
            },
            moveDirection: function() {
                var a;
                0 > this.newRelativeX ? (a = "right", this.playDirection = "next") : (a = "left", this.playDirection = "prev");
                return a
            },
            customEvents: function() {
                var a = this;
                a.$elem.on("owl.next", function() {
                    a.next()
                });
                a.$elem.on("owl.prev", function() {
                    a.prev()
                });
                a.$elem.on("owl.play", function(b, e) {
                    a.options.autoPlay = e;
                    a.play();
                    a.hoverStatus = "play"
                });
                a.$elem.on("owl.stop", function() {
                    a.stop();
                    a.hoverStatus = "stop"
                });
                a.$elem.on("owl.goTo", function(b, e) {
                    a.goTo(e)
                });
                a.$elem.on("owl.jumpTo", function(b, e) {
                    a.jumpTo(e)
                })
            },
            stopOnHover: function() {
                var a = this;
                !0 === a.options.stopOnHover && !0 !== a.browser.isTouch && !1 !== a.options.autoPlay && (a.$elem.on("mouseover", function() {
                    a.stop()
                }), a.$elem.on("mouseout", function() {
                    "stop" !== a.hoverStatus && a.play()
                }))
            },
            lazyLoad: function() {
                var a, b, e, c, d;
                if (!1 === this.options.lazyLoad) return !1;
                for (a = 0; a < this.itemsAmount; a += 1) b = f(this.$owlItems[a]), "loaded" !== b.data("owl-loaded") && (e = b.data("owl-item"), c = b.find(".lazyOwl"), "string" !== typeof c.data("src") ? b.data("owl-loaded", "loaded") : (void 0 === b.data("owl-loaded") && (c.hide(), b.addClass("loading").data("owl-loaded", "checked")), (d = !0 === this.options.lazyFollow ? e >= this.currentItem : !0) && e < this.currentItem + this.options.items && c.length && this.lazyPreload(b, c)))
            },
            lazyPreload: function(a, b) {
                function e() {
                    a.data("owl-loaded", "loaded").removeClass("loading");
                    b.removeAttr("data-src");
                    "fade" === d.options.lazyEffect ? b.fadeIn(400) : b.show();
                    "function" === typeof d.options.afterLazyLoad && d.options.afterLazyLoad.apply(this, [d.$elem])
                }

                function c() {
                    f += 1;
                    d.completeImg(b.get(0)) || !0 === k ? e() : 100 >= f ? g.setTimeout(c, 100) : e()
                }
                var d = this,
                    f = 0,
                    k;
                "DIV" === b.prop("tagName") ? (b.css("background-image", "url(" + b.data("src") + ")"), k = !0) : b[0].src = b.data("src");
                c()
            },
            autoHeight: function() {
                function a() {
                    var a = f(e.$owlItems[e.currentItem]).height();
                    e.wrapperOuter.css("height", a + "px");
                    e.wrapperOuter.hasClass("autoHeight") || g.setTimeout(function() {
                        e.wrapperOuter.addClass("autoHeight")
                    }, 0)
                }

                function b() {
                    d += 1;
                    e.completeImg(c.get(0)) ? a() : 100 >= d ? g.setTimeout(b, 100) : e.wrapperOuter.css("height", "")
                }
                var e = this,
                    c = f(e.$owlItems[e.currentItem]).find("img"),
                    d;
                void 0 !== c.get(0) ? (d = 0, b()) : a()
            },
            completeImg: function(a) {
                return !a.complete || "undefined" !== typeof a.naturalWidth && 0 === a.naturalWidth ? !1 : !0
            },
            onVisibleItems: function() {
                var a;
                !0 === this.options.addClassActive && this.$owlItems.removeClass("active");
                this.visibleItems = [];
                for (a = this.currentItem; a < this.currentItem + this.options.items; a += 1) this.visibleItems.push(a), !0 === this.options.addClassActive && f(this.$owlItems[a]).addClass("active");
                this.owl.visibleItems = this.visibleItems
            },
            transitionTypes: function(a) {
                this.outClass = "owl-" + a + "-out";
                this.inClass = "owl-" + a + "-in"
            },
            singleItemTransition: function() {
                var a = this,
                    b = a.outClass,
                    e = a.inClass,
                    c = a.$owlItems.eq(a.currentItem),
                    d = a.$owlItems.eq(a.prevItem),
                    f = Math.abs(a.positionsInArray[a.currentItem]) + a.positionsInArray[a.prevItem],
                    g = Math.abs(a.positionsInArray[a.currentItem]) + a.itemWidth / 2;
                a.isTransition = !0;
                a.$owlWrapper.addClass("owl-origin").css({
                    "-webkit-transform-origin": g + "px",
                    "-moz-perspective-origin": g + "px",
                    "perspective-origin": g + "px"
                });
                d.css({
                    position: "relative",
                    left: f + "px"
                }).addClass(b).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function() {
                    a.endPrev = !0;
                    d.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend");
                    a.clearTransStyle(d, b)
                });
                c.addClass(e).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function() {
                    a.endCurrent = !0;
                    c.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend");
                    a.clearTransStyle(c, e)
                })
            },
           
            
            clearEvents: function() {
                this.$elem.off(".owl owl mousedown.disableTextSelect");
                f(k).off(".owl owl");
                f(g).off("resize", this.resizer)
            },
            unWrap: function() {
                0 !== this.$elem.children().length && (this.$owlWrapper.unwrap(), this.$userItems.unwrap().unwrap(), this.owlControls && this.owlControls.remove());
                this.clearEvents();
                this.$elem.attr("style", this.$elem.data("owl-originalStyles") || "").attr("class", this.$elem.data("owl-originalClasses"))
            },
            removeItem: function(a) {
                if (0 === this.$elem.children().length) return !1;
                a = void 0 === a || -1 === a ? -1 : a;
                this.unWrap();
                this.$userItems.eq(a).remove();
                this.setVars()
            }
        };
     
        f.fn.owlCarousel.options = {
            items: 5,
            itemsCustom: !1,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 3],
            itemsTablet: [768, 2],
            itemsTabletSmall: !1,
            itemsMobile: [479, 1],
            singleItem: !1,
            itemsScaleUp: !1,
            slideSpeed: 200,
            paginationSpeed: 800,
            rewindSpeed: 1E3,
            autoPlay: !1,
            stopOnHover: !1,
            navigation: !1,
            navigationText: ["prev", "next"],
            rewindNav: !0,
            scrollPerPage: !1,
            pagination: !0,
            paginationNumbers: !1,
            responsive: !0,
            responsiveRefreshRate: 200,
            responsiveBaseWidth: g,
            baseClass: "owl-carousel",
            theme: "owl-theme",
            lazyLoad: !1,
            lazyFollow: !0,
            lazyEffect: "fade",
            autoHeight: !1,
            jsonPath: !1,
            jsonSuccess: !1,
            dragBeforeAnimFinish: !0,
            mouseDrag: !0,
            touchDrag: !0,
            addClassActive: !1,
            transitionStyle: !1,
            beforeUpdate: !1,
            afterUpdate: !1,
            beforeInit: !1,
            afterInit: !1,
            beforeMove: !1,
            afterMove: !1,
            afterAction: !1,
            startDragging: !1,
            afterLazyLoad: !1
        }
    })(jQuery, window, document);
} catch (e) {
  
};;