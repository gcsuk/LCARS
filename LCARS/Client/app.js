import Build from './js/build.js';

let body = document.querySelector('body');

body.textContent = 'Good point: ' + new Build(1, 23);
