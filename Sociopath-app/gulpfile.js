var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');

gulp.task('build', function () {
    var bundleStream = browserify({
        entries: './www/js/index.js'
    }).bundle();

    bundleStream
        .pipe(source('app.js'))
        .pipe(gulp.dest('./www/js/'));
});