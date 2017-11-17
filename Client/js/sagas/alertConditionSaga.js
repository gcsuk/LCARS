import { delay } from 'redux-saga';
import { put, fork, select } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import { setAlert } from '../actions';

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