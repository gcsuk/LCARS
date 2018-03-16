import { delay } from 'redux-saga';
import getData from '../fetch';
import { put, takeLatest } from 'redux-saga/effects';
import { refreshDeployments, REFRESH_DEPLOYMENTS } from '../actions';

export function* deploymentsSaga () {
    while (true) {
        const response = yield getData('deployments');
        yield put(refreshDeployments(response));
        yield delay(5000);
    }
}

export function* watchGetDeployments() {
    yield takeLatest(REFRESH_DEPLOYMENTS, deploymentsSaga);
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