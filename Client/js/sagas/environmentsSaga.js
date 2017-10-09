import { delay } from 'redux-saga';
import fetch from 'unfetch';
import { put } from 'redux-saga/effects';
import { refreshEnvironments, REFRESH_ENVIRONMENTS } from '../actions';

export function* environmentsSaga () {
    while (true) {
        const response = yield getEnvironmentsData();
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
      var error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}

function getEnvironmentsData() {
    return fetch('http://localhost:54359/api/environments/')
            .then(res => res.json());
}