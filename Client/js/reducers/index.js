import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';

import builds from './builds';
import deployments from './deployments';
import environments from './environments';
import git from './git';
import issues from './issues';
import issueSummary from './issueSummary';
import alertCondition from './alertCondition';

const rootReducer = combineReducers({ builds, deployments, environments, git, issues, issueSummary, alertCondition, routing: routerReducer});

export default rootReducer;