"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    uglify = require("gulp-uglify");
var gutil = require('gulp-util');

var paths = {
    webroot: "./wwwroot/",
    wlcmJsPaths: [
        "./wwwroot/js/welcomePage.js"
    ],
    wlcmCssPaths: [
            "./wwwroot/css/main.css",
        "./wwwroot/css/welcomePage.css"
    ],
    loginJsPaths: [
        "./wwwroot/js/jquery.form.js",
        "./wwwroot/js/smartajaxforms.js",
        "./wwwroot/js/portal/account/loginregisterforgot.js",
        "./wwwroot/js/portal/manage/deleteaccount.js",
        "./wwwroot/js/portal/manage/resetpassword.js"
    ],
    loginCssPaths: [
        "./wwwroot/css/main.css",
        "./wwwroot/css/portal/account/loginregisterforgot.css",
        "./wwwroot/css/portal/manage/resetpassword.css"
    ],
    portalJsPaths: [
        "./wwwroot/js/dashboard/plugins/ionrangeslider/js/ion.rangeslider.js",
        "./wwwroot/js/dashboard/plugins/angular-materialize.js",
        "./wwwroot/js/Dashboard/plugins/perfect-scrollbar/perfect-scrollbar.min.js",
        "./wwwroot/js/Dashboard/plugins/chartjs/chart.min.js",
        "./wwwroot/js/Dashboard/plugins/dropify/js/dropify.min.js",
        "./wwwroot/lib/angular-perfect-scrollbar/src/angular-perfect-scrollbar.js",
        "./wwwroot/lib/angular-chart.js/dist/angular-chart.js",
        "./wwwroot/js/PortalApp/**/*.js",
        "./wwwroot/js/Dashboard/custom-sidenav-shrink-helper.js",
        "./wwwroot/js/Dashboard/plugins.js"
    ],
    portalCssPaths: [
        "./wwwroot/css/Dashboard/materialize.css",
        "./wwwroot/css/Dashboard/style.css",
        "./wwwroot/css/Dashboard/custom/custom.css",
        "./wwwroot/js/Dashboard/plugins/perfect-scrollbar/perfect-scrollbar.css",
        "./wwwroot/js/Dashboard/plugins/dropify/css/dropify.css",
        "./wwwroot/js/dashboard/plugins/ionrangeslider/css/ion.rangeslider.css",
        "./wwwroot/js/dashboard/plugins/ionrangeslider/css/ion.rangeslider.skinflat.css",
        "./wwwroot/lib/angular-chart.js/dist/angular-chart.css"
    ]
};

paths.concatWlcmJsDest = paths.webroot + "js/wlcm.min.js";
paths.concatWlcmCssDest = paths.webroot + "css/wlcm.min.css";
paths.concatLoginJsDest = paths.webroot + "js/login.min.js";
paths.concatLoginCssDest = paths.webroot + "css/login.min.css";
paths.concatPortalJsDest = paths.webroot + "js/portal.min.js";
paths.concatPortalCssDest = paths.webroot + "css/portal.min.css";
paths.sass = "./Content/SCSS/**/*.scss";
paths.destCss = paths.webroot + "css/";

gulp.task("min:wlcm:js", function () {
    return gulp.src(paths.wlcmJsPaths)
        .pipe(concat(paths.concatWlcmJsDest))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task("min:wlcm:css", function () {
    return gulp.src(paths.wlcmCssPaths)
        .pipe(concat(paths.concatWlcmCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
});

gulp.task("min:login:js", function () {
    return gulp.src(paths.loginJsPaths)
        .pipe(concat(paths.concatLoginJsDest))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task("min:login:css", function () {
    return gulp.src(paths.loginCssPaths)
        .pipe(concat(paths.concatLoginCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
});

gulp.task("min:portal:js", function () {
    return gulp.src(paths.portalJsPaths)
        .pipe(concat(paths.concatPortalJsDest))
        .pipe(uglify().on('error', gutil.log))
        .pipe(gulp.dest('.'));
});

gulp.task("min:portal:css", function () {
    return gulp.src(paths.portalCssPaths)
        .pipe(concat(paths.concatPortalCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
});


gulp.task("min", ["min:wlcm:js", "min:wlcm:css", "min:login:js", "min:login:css", "min:portal:css","min:portal:js"]);

gulp.task('sass', function () {
    gulp.src(paths.sass)
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest(paths.destCss));
});

gulp.task('sass:watch', function () {
    gulp.watch(paths.sass, ['sass']);
});