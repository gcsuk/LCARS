import { delay } from 'redux-saga';
import fetch from 'unfetch';
import { put } from 'redux-saga/effects';
import { refreshDeployments, REFRESH_DEPLOYMENTS } from '../actions';

export function* deploymentsSaga () {
    while (true) {
        const response = yield getDeploymentsData();
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
      var error = new Error(response.statusText);
      error.response = response;
      return Promise.reject(error);
    }
}

function getDeploymentsData() {
    return fetch('http://localhost:54359/api/deployments/')
            .then(res => res.json());
}