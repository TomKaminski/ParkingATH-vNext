(function (angular) {
    angular.module("ui.materialize", ["ui.materialize.input_date", "ui.materialize.pagination","ui.materialize.modal"]);

 

    /**
     * Add pickadate directive
     * Type text is mandatory
     * Example:
     <input input-date
        type="text"
        name="created"
        id="inputCreated"
        ng-model="currentTime"
        format="dd/mm/yyyy"
        months-full="{{ monthFr }}"
        months-short="{{ monthShortFr }}"
        weekdays-full="{{ weekdaysFullFr }}"
        weekdays-short="{{ weekdaysShortFr }}"
        weekdays-letter="{{ weekdaysLetterFr }}"
        disable="disable"
        today="today"
        clear="clear"
        close="close"
        on-start="onStart()"
        on-render="onRender()"
        on-open="onOpen()"
        on-close="onClose()"
        on-set="onSet()"
        on-stop="onStop()" />
     */
    angular.module("ui.materialize.input_date", [])
        .directive('inputDate', ["$compile", "$timeout", function ($compile, $timeout) {
            // Fix for issue 46. This gotta be a bug in the materialize code, but this fixes it.
            var style = $('<style>#inputCreated_root {outline: none;}</style>');
            $('html > head').append(style);

            // Define Prototype Date format
            // Use like this
            // today = new Date();
            // var dateString = today.format("dd-m-yy");
            var dateFormat = function () {

                var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
                    timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
                    timezoneClip = /[^-+\dA-Z]/g,
                    pad = function (val, len) {
                        val = String(val);
                        len = len || 2;
                        while (val.length < len) {
                            val = "0" + val;
                        }
                        return val;
                    };

                // Regexes and supporting functions are cached through closure
                return function (date, mask, utc) {

                    var dF = dateFormat;

                    // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
                    if (arguments.length === 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
                        mask = date;
                        date = undefined;
                    }

                    // Passing date through Date applies Date.parse, if necessary
                    date = date ? new Date(date) : new Date();
                    if (isNaN(date)) throw SyntaxError("invalid date");

                    mask = String(dF.masks[mask] || mask || dF.masks["default"]);

                    // Allow setting the utc argument via the mask
                    if (mask.slice(0, 4) == "UTC:") {
                        mask = mask.slice(4);
                        utc = true;
                    }

                    var _ = utc ? "getUTC" : "get",
                        d = date[ _ + "Date" ](),
                        D = date[ _ + "Day" ](),
                        m = date[ _ + "Month" ](),
                        y = date[ _ + "FullYear" ](),
                        H = date[ _ + "Hours" ](),
                        M = date[ _ + "Minutes" ](),
                        s = date[ _ + "Seconds" ](),
                        L = date[ _ + "Milliseconds" ](),
                        o = utc ? 0 : date.getTimezoneOffset(),
                        flags = {
                            d:    d,
                            dd:   pad(d),
                            ddd:  dF.i18n.dayNames[D],
                            dddd: dF.i18n.dayNames[D + 7],
                            m:    m + 1,
                            mm:   pad(m + 1),
                            mmm:  dF.i18n.monthNames[m],
                            mmmm: dF.i18n.monthNames[m + 12],
                            yy:   String(y).slice(2),
                            yyyy: y,
                            h:    H % 12 || 12,
                            hh:   pad(H % 12 || 12),
                            H:    H,
                            HH:   pad(H),
                            M:    M,
                            MM:   pad(M),
                            s:    s,
                            ss:   pad(s),
                            l:    pad(L, 3),
                            L:    pad(L > 99 ? Math.round(L / 10) : L),
                            t:    H < 12 ? "a"  : "p",
                            tt:   H < 12 ? "am" : "pm",
                            T:    H < 12 ? "A"  : "P",
                            TT:   H < 12 ? "AM" : "PM",
                            Z:    utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                            o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                            S:    ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
                        };

                    return mask.replace(token, function ($0) {
                        return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
                    });
                };
            }();

            // Some common format strings
            dateFormat.masks = {
               "default":      "ddd mmm dd yyyy HH:MM:ss",
                shortDate:      "m/d/yy",
                mediumDate:     "mmm d, yyyy",
                longDate:       "mmmm d, yyyy",
                fullDate:       "dddd, mmmm d, yyyy",
                shortTime:      "h:MM TT",
                mediumTime:     "h:MM:ss TT",
                longTime:       "h:MM:ss TT Z",
                isoDate:        "yyyy-mm-dd",
                isoTime:        "HH:MM:ss",
                isoDateTime:    "yyyy-mm-dd'T'HH:MM:ss",
                isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
            };

            // Internationalization strings
            dateFormat.i18n = {
                dayNames: [
                    "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
                    "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
                ],
                monthNames: [
                    "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
                ]
            };

            // For convenience...
            Date.prototype.format = function (mask, utc) {
                return dateFormat(this, mask, utc);
            };

            /**
             * Validate date object
             * @param  {Date}  date
             * @return {Boolean}
             */
            var isValidDate = function(date) {
                if( Object.prototype.toString.call(date) === '[object Date]' ) {
                    return !isNaN(date.getTime());
                } 
                return false;
            };

            return {
                require: 'ngModel',
                scope: {
                    container: "@",
                    format: "@",
                    formatSubmit: "@",
                    monthsFull: "@",
                    monthsShort: "@",
                    weekdaysFull: "@",
                    weekdaysLetter: "@",
                    disable: "=",
                    today: "=",
                    clear: "=",
                    close: "=",
                    selectYears: "=",
                    onStart: "&",
                    onRender: "&",
                    onOpen: "&",
                    onClose: "&",
                    onSet: "&",
                    onStop: "&",
                    ngReadonly: "=?",
                    max: "@",
                    min: "@"
                },
                link: function (scope, element, attrs, ngModelCtrl) {

                    ngModelCtrl.$formatters.unshift(function (modelValue) {
                        if (modelValue) {
                            var date = new Date(modelValue);
                            return (angular.isDefined(scope.format)) ? date.format(scope.format) : date.format('d mmmm, yyyy');
                        }
                        return null;
                    });

                    var monthsFull = (angular.isDefined(scope.monthsFull)) ? scope.$eval(scope.monthsFull) : undefined,
                        monthsShort = (angular.isDefined(scope.monthsShort)) ? scope.$eval(scope.monthsShort) : undefined,
                        weekdaysFull = (angular.isDefined(scope.weekdaysFull)) ? scope.$eval(scope.weekdaysFull) : undefined,
                        weekdaysLetter = (angular.isDefined(scope.weekdaysLetter)) ? scope.$eval(scope.weekdaysLetter) : undefined;

                    function hasClass(el, className) {
                        if (el.classList)
                            return el.classList.contains(className)
                        else
                            return !!el.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'))
                    }

                    function removeClass(el, className) {
                        if (el.classList)
                            el.classList.remove(className)
                        else if (hasClass(el, className)) {
                            var reg = new RegExp('(\\s|^)' + className + '(\\s|$)')
                            el.className = el.className.replace(reg, ' ')
                        }
                    }

                    function addClass(el, className) {
                        if (el.classList)
                            el.classList.add(className)
                        else if (!hasClass(el, className)) el.className += " " + className
                    }

                    $compile(element.contents())(scope);
                    if (!(scope.ngReadonly)) {
                        $timeout(function () {
                            var pickadateInput = element.pickadate({
                                container: (angular.isDefined(scope.container)) ? scope.container : 'body',
                                format: (angular.isDefined(scope.format)) ? scope.format : undefined,
                                formatSubmit: (angular.isDefined(scope.formatSubmit)) ? scope.formatSubmit : undefined,
                                monthsFull: (angular.isDefined(monthsFull)) ? monthsFull : undefined,
                                monthsShort: (angular.isDefined(monthsShort)) ? monthsShort : undefined,
                                weekdaysFull: (angular.isDefined(weekdaysFull)) ? weekdaysFull : undefined,
                                weekdaysLetter: (angular.isDefined(weekdaysLetter)) ? weekdaysLetter : undefined,
                                disable: (angular.isDefined(scope.disable)) ? scope.disable : undefined,
                                today: (angular.isDefined(scope.today)) ? scope.today : undefined,
                                clear: (angular.isDefined(scope.clear)) ? scope.clear : undefined,
                                close: (angular.isDefined(scope.close)) ? scope.close : undefined,
                                selectYears: (angular.isDefined(scope.selectYears)) ? scope.selectYears : undefined,
                                onStart: (angular.isDefined(scope.onStart)) ? function() { scope.onStart(); } : undefined,
                                onRender: (angular.isDefined(scope.onRender)) ? function() { scope.onRender(); } : undefined,
                                onOpen: (angular.isDefined(scope.onOpen)) ? function() { scope.onOpen(); } : undefined,
                                onClose: (angular.isDefined(scope.onClose)) ? function() {
                                    removeClass(element[0], 'ng-touched');
                                    addClass(element[0], 'ng-untouched');
                                    removeClass(element.context, 'ng-touched');
                                    addClass(element.context, 'ng-untouched');
                                    element[0].blur();
                                    element.blur();
                                    element.context.blur();
                                    scope.onClose();
                                } : undefined,
                                onSet: (angular.isDefined(scope.onSet)) ? function() { scope.onSet(); } : undefined,
                                onStop: (angular.isDefined(scope.onStop)) ? function() { scope.onStop(); } : undefined
                            });

                            var picker = pickadateInput.pickadate('picker').close(false);
                            //watcher of min and max
                            scope.$watch('max', function(newMax) {
                                if (picker) {
                                    var maxDate = new Date(newMax);
                                    picker.set({ max: isValidDate(maxDate) ? maxDate : false });
                                }
                            });
                            scope.$watch('min', function(newMin) {
                                if (picker) {
                                    var minDate = new Date(newMin);
                                    picker.set({ min: isValidDate(minDate) ? minDate : false });
                                }
                            });
                        });
                    }
                }
            };
        }]);

    /**
     * Example:
     <pagination
        page="1"
        page-size="10"
        total="100"
        pagination-action="changePage(page)"
        ul-class="customClass">
     * ul-class could be either an object or a string
     *
     * Based on https://github.com/brantwills/Angular-Paging
     */
    angular.module("ui.materialize.pagination", [])
        .directive('pagination', function () {

            // Assign null-able scope values from settings
            function setScopeValues(scope, attrs) {
                scope.List = [];
                scope.Hide = false;
                scope.page = parseInt(scope.page) || 1;
                scope.total = parseInt(scope.total) || 0;
                scope.dots = scope.dots || '...';
                scope.ulClass = scope.ulClass || attrs.ulClass || 'pagination';
                scope.adjacent = parseInt(scope.adjacent) || 2;
                scope.activeClass = 'active';
                scope.disabledClass = 'disabled';

                scope.scrollTop = scope.$eval(attrs.scrollTop);
                scope.hideIfEmpty = scope.$eval(attrs.hideIfEmpty);
                scope.showPrevNext = scope.$eval(attrs.showPrevNext);
            }

            // Validate and clean up any scope values
            // This happens after we have set the
            // scope values
            function validateScopeValues(scope, pageCount) {
                // Block where the page is larger than the pageCount
                if (scope.page > pageCount) {
                    scope.page = pageCount;
                }

                // Block where the page is less than 0
                if (scope.page <= 0) {
                    scope.page = 1;
                }

                // Block where adjacent value is 0 or below
                if (scope.adjacent <= 0) {
                    scope.adjacent = 2;
                }

                // Hide from page if we have 1 or less pages
                // if directed to hide empty
                if (pageCount <= 1) {
                    scope.Hide = scope.hideIfEmpty;
                }
            }

            // Internal Pagination Click Action
            function internalAction(scope, page) {
                // Block clicks we try to load the active page
                if (scope.page == page) {
                    return;
                }

                // Update the page in scope and fire any paging actions
                scope.page = page;
                scope.paginationAction({
                    page: page
                });

                // If allowed scroll up to the top of the page
                if (scope.scrollTop) {
                    scrollTo(0, 0);
                }
            }

            // Add Range of Numbers
            function addRange(start, finish, scope) {
                var i = 0;
                for (i = start; i <= finish; i++) {
                    var item = {
                        value: i.toString(),
                        liClass: scope.page == i ? scope.activeClass : 'waves-effect',
                        action: function() {
                            internalAction(scope, this.value);
                        }
                    };

                    scope.List.push(item);
                }
            }

            // Add Dots ie: 1 2 [...] 10 11 12 [...] 56 57
            function addDots(scope) {
                scope.List.push({
                    value: scope.dots
                });
            }

            // Add First Pages
            function addFirst(scope, next) {
                addRange(1, 2, scope);

                // We ignore dots if the next value is 3
                // ie: 1 2 [...] 3 4 5 becomes just 1 2 3 4 5
                if (next != 3) {
                    addDots(scope);
                }
            }

            /**
            * Add the first, previous, next, and last buttons if desired   
            * The logic is defined by the mode of interest
            * This method will simply return if the scope.showPrevNext is false
            * This method will simply return if there are no pages to display
            *
            * @param {Object} scope - The local directive scope object
            * @param {int} pageCount - The last page number or total page count
            * @param {string} mode - The mode of interest either prev or last 
            */
            function addPrevNext(scope, pageCount, mode){
                
                // Ignore if we are not showing
                // or there are no pages to display
                if (!scope.showPrevNext || pageCount < 1) { return; }

                // Local variables to help determine logic
                var disabled, alpha, beta;


                // Determine logic based on the mode of interest
                // Calculate the previous / next page and if the click actions are allowed
                if(mode === 'prev') {
                    
                    disabled = scope.page - 1 <= 0;
                    var prevPage = scope.page - 1 <= 0 ? 1 : scope.page - 1;
                    
                    alpha = { value : "<<", title: 'First Page', page: 1 };
                    beta = { value: "<", title: 'Previous Page', page: prevPage };
                     
                } else {
                    
                    disabled = scope.page + 1 > pageCount;
                    var nextPage = scope.page + 1 >= pageCount ? pageCount : scope.page + 1;
                    
                    alpha = { value : ">", title: 'Next Page', page: nextPage };
                    beta = { value: ">>", title: 'Last Page', page: pageCount };
                }

                // Create the Add Item Function
                var addItem = function(item, disabled){           
                    scope.List.push({
                        value: item.value,
                        title: item.title,
                        liClass: disabled ? scope.disabledClass : '',
                        action: function(){
                            if(!disabled) {
                                internalAction(scope, item.page);
                            }
                        }
                    });
                };

                // Add our items
                addItem(alpha, disabled);
                addItem(beta, disabled);
            }

            function addLast(pageCount, scope, prev) {
                // We ignore dots if the previous value is one less that our start range
                // ie: 1 2 3 4 [...] 5 6  becomes just 1 2 3 4 5 6
                if (prev != pageCount -2) {
                    addDots(scope);
                }

                addRange(pageCount -1, pageCount, scope);
            }

            // Main build function
            function build(scope, attrs) {

                // Block divide by 0 and empty page size
                if (!scope.pageSize || scope.pageSize < 0)
                {
                    return;
                }

                // Assign scope values
                setScopeValues(scope, attrs);

                // local variables
                var start,
                    size = scope.adjacent * 2,
                    pageCount = Math.ceil(scope.total / scope.pageSize);

                // Validation Scope
                validateScopeValues(scope, pageCount);

                // Add the Next and Previous buttons to our list
                addPrevNext(scope, pageCount, 'prev');

                if (pageCount < (5 + size)) {

                    start = 1;
                    addRange(start, pageCount, scope);

                } else {

                    var finish;

                    if (scope.page <= (1 + size)) {

                        start = 1;
                        finish = 2 + size + (scope.adjacent - 1);

                        addRange(start, finish, scope);
                        addLast(pageCount, scope, finish);

                    } else if (pageCount - size > scope.page && scope.page > size) {

                        start = scope.page - scope.adjacent;
                        finish = scope.page + scope.adjacent;

                        addFirst(scope, start);
                        addRange(start, finish, scope);
                        addLast(pageCount, scope, finish);

                    } else {

                        start = pageCount - (1 + size + (scope.adjacent - 1));
                        finish = pageCount;

                        addFirst(scope, start);
                        addRange(start, finish, scope);

                    }
                }
                addPrevNext(scope, pageCount, 'next');
            }

            return {
                restrict: 'EA',
                scope: {
                    page: '=',
                    pageSize: '=',
                    total: '=',
                    dots: '@',
                    hideIfEmpty: '@',
                    adjacent: '@',
                    scrollTop: '@',
                    showPrevNext: '@',
                    paginationAction: '&',
                    ulClass: '=?'
                },
                template:
                    '<ul ng-hide="Hide" ng-class="ulClass"> ' +
                        '<li ' +
                        'ng-class="Item.liClass" ' +
                        'ng-click="Item.action()" ' +
                        'ng-repeat="Item in List"> ' +
                        '<a href> ' +
                        '<span ng-bind="Item.value"></span> ' +
                        '</a>' +
                    '</ul>',
                link: function (scope, element, attrs) {

                    // Hook in our watched items
                    scope.$watchCollection('[page, total, pageSize]', function () {
                        build(scope, attrs);
                    });
                }
            };
        });

    /*     example usage:
   <!-- Modal Trigger -->
   <a class='btn' href='#demoModal' modal>show Modal</a>
   <!-- Modal Structure -->
   <div id="demoModal" class="modal">
   <div class="modal-content">
   <h4>Modal Header</h4>
   <p>A bunch of text</p>
   </div>
   <div class="modal-footer">
   <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">Agree</a>
   </div>
   </div>
   */
    angular.module("ui.materialize.modal", [])
        .directive("modal", ["$compile", "$timeout", function ($compile, $timeout) {
            return {
                scope: {
                    dismissible: "=",
                    opacity: "@",
                    inDuration: "@",
                    outDuration: "@"
                },
                link: function (scope, element, attrs) {
                    $compile(element.contents())(scope);
                    $timeout(function () {
                        element.leanModal({
                            dismissible: (angular.isDefined(scope.dismissible)) ? scope.dismissible : undefined,
                            opacity: (angular.isDefined(scope.opacity)) ? scope.opacity : undefined,
                            in_duration: (angular.isDefined(scope.inDuration)) ? scope.inDuration : undefined,
                            out_duration: (angular.isDefined(scope.outDuration)) ? scope.outDuration : undefined
                        });
                    });
                }
            };
        }]);

}(angular));