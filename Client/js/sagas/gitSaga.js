import { delay } from 'redux-saga';
import fetch from 'unfetch';
import { put } from 'redux-saga/effects';
import { refreshGit, REFRESH_GIT } from '../actions';

export function* gitSaga () {
    while (true) {
        const response = yield getGitData();
        yield put(refreshGit(response));
        yield delay(5000);
    }
}

export function* watchGetGit() {
    yield takeLatest(REFRESH_GIT, gitSaga);
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

function getGitData() {
    return fetch('http://localhost:54359/api/github/summary')
            .then(res => res.json());
}