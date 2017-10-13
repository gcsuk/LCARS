import { delay } from 'redux-saga';
import { put, fork, select } from 'redux-saga/effects';
import { push } from 'react-router-redux';

function* routeTimer() {
  let currentIndex = 0;
  const paths = [
    '/builds',
    '/deployments',
    '/git',
    '/environments',
    '/issues',
    '/issuesummary'
  ];

  while(true) {
    const { routeChangeInterval } = yield select(state => state.config);
    const route = paths[currentIndex];
    yield put(push(route));
    currentIndex < paths.length ? currentIndex++ : currentIndex = 0;
    yield delay(routeChangeInterval);
  }
}

export function* watchRoute() {
  const timerFork = yield fork(routeTimer);
}