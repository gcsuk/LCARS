import { delay } from 'redux-saga';
import getData from '../fetch';
import { put, takeLatest } from 'redux-saga/effects';
import { refreshEnvironments, REFRESH_ENVIRONMENTS } from '../actions';

export function* environmentsSaga () {
    while (true) {
        const response = yield getData('environments');
        yield put(refreshEnvironments(response));
        yield delay(5000);
    }
}

export function* watchGetEnvironments() {
    yield takeLatest(REFRESH_ENVIRONMENTS, environmentsSaga);
}

function checkStatus(response) {
    if (response.ok)
      return response;
    else {
      const error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}