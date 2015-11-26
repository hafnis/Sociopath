var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var sourcemaps = require('gulp-sourcemaps');

gulp.task('build', function () {
    var bundleStream = browserify({
        entries: './www/js/index.js',
		debug: true
    }).bundle();

    bundleStream
        .pipe(source('app.js'))
		.pipe(buffer())
        .pipe(sourcemaps.init({loadMaps: true})) 
		.pipe(sourcemaps.write())
		.pipe(gulp.dest('./www/js/'))
});