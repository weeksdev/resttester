var gulp = require('gulp'),
    msbuild = require('gulp-msbuild'),
    githubRelease = require('gulp-github-release'),
    zip = require('gulp-zip'),
    series = require('run-sequence'),
    fs = require('fs');

gulp.task('build', function () {
    return gulp.src('./RestTester.sln')
        .pipe(msbuild({
            Configuration: 'Release',
            errorOnFail: true,
        }));
});
gulp.task('zip', function () {
    return gulp.src('RestTester/bin/Release/*')
    .pipe(zip('RestTester.zip'))
    .pipe(gulp.dest('dist'));
})
gulp.task('github-release', function () {
    var token = fs.readFileSync('c:/projects/github/token.txt');
    return gulp.src('dist/RestTester.zip')
    .pipe(githubRelease({
        token: token,
        owner: 'weeksdev',
        repo: 'resttester',
        tag: 'v1.0.0',
        name: 'resttester-release v1.0.0',
        notes: 'build-release',
        draft: false,
        prerelease: false
    }))
});
gulp.task('deploy', function () {
    series('build', 'zip', 'github-release')
});