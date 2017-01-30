
var gulp = require('gulp');
var vulcanize = require('gulp-vulcanize');
var del = require('del');

gulp.task('prepublish', function(cb){
    _clean();
    _vulcanize();
    _copyAssets();
});

function _clean(){
    del.sync('dist/**');
}

function _vulcanize(){
    return gulp.src('src/index.html')
        .pipe(vulcanize({
            abspath: '',
            excludes: [],
            stripExcludes: false,
            redirects: [
                '../api|/Kantilever/api'
            ]
        }))
        .pipe(gulp.dest('dist'));
}

function _copyAssets(){
    gulp.src('src/bower_components/bootstrap/dist/**')
        .pipe(gulp.dest('dist/bower_components/bootstrap/dist'));
    gulp.src('src/bower_components/webcomponentsjs/**')
        .pipe(gulp.dest('dist/bower_components/webcomponentsjs'));
    gulp.src('src/bower_components/web-animations-js/**')
        .pipe(gulp.dest('dist/bower_components/web-animations-js'));
    gulp.src('src/bower_components/promise-polyfill/**')
        .pipe(gulp.dest('dist/bower_components/promise-polyfill'));
    gulp.src('src/css/**')
        .pipe(gulp.dest('dist/css'));
    gulp.src('src/images/**')
        .pipe(gulp.dest('dist/images'));
    gulp.src('src/files/**')
        .pipe(gulp.dest('dist/files'));
    gulp.src('src/style.css')
        .pipe(gulp.dest('dist'));
}