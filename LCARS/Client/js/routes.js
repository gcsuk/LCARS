import React from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './containers/App';
import HomePage from './components/HomePage';
import BuildsPage from './components/BuildsPage';
import DeploymentsPage from './components/DeploymentsPage';
import GitPage from './components/GitPage';
import EnvironmentsPage from './components/EnvironmentsPage';
import IssuesPage from './components/IssuesPage';
import IssueSummaryPage from './components/IssueSummaryPage';
import RedAlertPage from './components/RedAlertPage';

export default (
  <Route path="/" component={App}>
    <IndexRoute component={DeploymentsPage} />
    <Route path="builds" component={BuildsPage} />
    <Route path="deployments" component={DeploymentsPage} />
    <Route path="git" component={GitPage} />
    <Route path="builds" component={BuildsPage} />
    <Route path="environments" component={EnvironmentsPage} />
    <Route path="issues" component={IssuesPage} />
    <Route path="alert" component={RedAlertPage} />
  </Route>
);
