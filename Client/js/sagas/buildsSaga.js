import { delay } from 'redux-saga';
import getData from '../fetch';
import { put, takeLatest } from 'redux-saga/effects';
import { refreshBuilds, REFRESH_BUILDS } from '../actions';

export function* buildsSaga () {
    while (true) {
        const response = yield getData('builds');
        const data = response.errors ? [] : response;
        yield put(refreshBuilds(data));
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
      let error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}