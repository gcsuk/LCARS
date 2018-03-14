import { delay } from 'redux-saga';
import getData from '../fetch';
import { put } from 'redux-saga/effects';
import { push, takeLatest } from 'react-router-redux';
import { refreshGit, REFRESH_GIT } from '../actions';

export function* gitSaga () {
    while (true) {
        const response = yield getData('github/summary');
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
      let error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}