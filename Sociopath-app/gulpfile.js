var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var sourcemaps = require('gulp-sourcemaps');
var watchify = require('watchify');
var jshint = require('gulp-jshint');
var uglify = require('gulp-uglify');
var minifyCss = require('gulp-minify-css');
var concatCss = require('gulp-concat-css');
var watch = require('gulp-watch');

gulp.task('build-js-dev', function () {
    var bundleStream = browserify({
        entries: './www/js/index.js',
		debug: true
    });
	var b = watchify(bundleStream);
	b.on('update', function() {rebundle();});
	
	rebundle();
	
	function rebundle() {
		lintScripts();
		return b.bundle()
			.pipe(source('app.js'))
			.pipe(buffer())
			.pipe(sourcemaps.init({loadMaps: true})) 
			.pipe(sourcemaps.write())
			.pipe(gulp.dest('./www/js/'))
	}
});

function lintScripts() {
	var scripts = [
	'./www/js/*.js',
	'./www/js/ViewModels/*.js',
	'!./www/js/app.js',
	'!./www/js/app-prod.js',
	'!./www/js/cordova.js'];
	return gulp.src(scripts)
		.pipe(jshint())
		.pipe(jshint.reporter('default'));
}

gulp.task('build-js-prod', function() {
	var bundleStream = browserify({
        entries: './www/js/index.js',
		debug: true
    });
	lintScripts();
	bundleStream.bundle()
		.pipe(source('app-prod.js'))
		.pipe(buffer())
		.pipe(uglify())
		.pipe(gulp.dest('./www/js/'));
	
});

gulp.task('build-css-dev', function() {
	var scripts = [
		'./www/css/jquery.mobile-1.4.5.min.css',
		'./www/css/index.css'
	];
	
	watch('./www/css/index.css', function() {concat();});
	concat();
	function concat() {
	 return gulp.src(scripts)
		.pipe(concatCss('app.css'))
		.pipe(gulp.dest('./www/css'));
	}
});

gulp.task('build-css-prod', function() {
	var scripts = [
		'./www/css/jquery.mobile-1.4.5.min.css',
		'./www/css/index.css'
	];

	return gulp.src(scripts) 
		.pipe(concatCss('app-prod.css'))
		.pipe(minifyCss({compatibility: 'ie8'}))		
		.pipe(gulp.dest('./www/css'));
	
});

gulp.task('build-web', ['build-js-prod', 'build-css-prod']);

