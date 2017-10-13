import { createStore, compose, applyMiddleware } from 'redux';
import createSagaMiddleware from 'redux-saga';
import createHistroy from 'history/createBrowserHistory';
import { routerMiddleware } from 'react-router-redux';

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

export const history = createHistroy({
    basename: '/'
});
const historyMiddleware = routerMiddleware(history);
const sagaMiddleware = createSagaMiddleware();

const middlewares = [
    sagaMiddleware,
    historyMiddleware
];

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(
    rootReducer,
    defaultState,
    composeEnhancers(applyMiddleware(...middlewares))
);

initSagas(sagaMiddleware);
export default store;