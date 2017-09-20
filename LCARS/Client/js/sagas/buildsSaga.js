import { delay } from 'redux-saga';
import fetch from 'unfetch';

export function* buildsSaga () {
    while (true) {
        yield delay(5000);
        
        fetch('/data/builds.json')
            .then( checkStatus )
            .then( r => r.json() )
            .then( data => {
                console.log(data);
            });
    }
}

function checkStatus(response) {
    if (response.ok)
      return response;
    else {
      var error = new Error(response.statusText);
      error.response = response;
      console.log(error.response);
      return Promise.reject(error);
    }
}