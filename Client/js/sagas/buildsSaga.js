import { delay } from 'redux-saga';
import fetch from 'unfetch';
import { put } from 'redux-saga/effects';
import { refreshBuilds, REFRESH_BUILDS } from '../actions';

export function* buildsSaga () {
    while (true) {
        const response = yield getBuildData();
        yield put(refreshBuilds(response));
        yield delay(5000);
    }
}

export function* watchGetBuilds() {
    yield takeLatest(REFRESH_BUILDS, buildsSaga);
}

function checkStatus(response) {
    if (response.ok)
      return response;
    else {
      var error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}

function getBuildData() {
    return fetch('http://localhost:54359/api/builds/')
            .then(res => res.json());
}