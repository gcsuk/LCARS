import { delay } from 'redux-saga';
import getData from '../fetch';
import { put, fork, select, takeLatest } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import { setAlert, SET_ALERT } from '../actions';

export function* alertConditionSaga () {
  while (true) {
    const response = yield getData('alertcondition');
    yield put(setAlert(response.condition));
    yield delay(5000);
  }
}

function* countdownTimer() {
  const alertCondition = yield select(state => state.alertCondition);

  if (alertCondition.condition !== 'red')
    return;

  const endDate = new Date(alertCondition.endDate);

  while (true) {
    const timeLeft = endDate - new Date();

    if (timeLeft <= 0) {
      yield put(setAlert('green'));
      yield put(push('/builds'));
      break;
    }

    yield delay(100);
  }
}

export function* alertCondition() {
  const countdown = yield fork(countdownTimer);
}

export function* watchGetAlertCondition() {
  yield takeLatest(SET_ALERT, alertConditionSaga);
}