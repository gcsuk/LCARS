import { createStore, compose, applyMiddleware } from 'redux';
import { syncHistoryWithStore } from 'react-router-redux';  
import { browserHistory } from 'react-router';
import createSagaMiddleware from 'redux-saga';

import rootReducer from './reducers/index';
import { initSagas } from './initSagas';

import deployments from '../data/deployments';
import builds from '../data/builds';
import environments from '../data/environments';
import git from '../data/git';
import issues from '../data/issues';
import {issueSummary} from '../data/issues';
import alertCondition from '../data/alertCondition';

const defaultState = {
    deployments,
    builds,
    environments,
    git,
    issues,
    issueSummary,
    alertCondition
};

const sagaMiddleware = createSagaMiddleware();

const middlewares = [
    sagaMiddleware
];

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(
    rootReducer,
    defaultState,
    composeEnhancers(applyMiddleware(...middlewares))
);

initSagas(sagaMiddleware);

export const history = syncHistoryWithStore(browserHistory, store);

export default store;