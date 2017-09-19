import React from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './containers/App';
import BuildsPage from './containers/BuildsPage';
import DeploymentsPage from './containers/DeploymentsPage';
import GitPage from './containers/GitPage';
import EnvironmentsPage from './containers/EnvironmentsPage';
import IssuesPage from './containers/IssuesPage';
import IssueSummaryPage from './containers/IssueSummaryPage';
import RedAlertPage from './containers/RedAlertPage';

export default (
  <Route path="/" component={App}>
    <IndexRoute component={DeploymentsPage} />
    <Route path="builds" component={BuildsPage} />
    <Route path="deployments" component={DeploymentsPage} />
    <Route path="git" component={GitPage} />
    <Route path="builds" component={BuildsPage} />
    <Route path="environments" component={EnvironmentsPage} />
    <Route path="issues" component={IssuesPage} />
    <Route path="issuesummary" component={IssueSummaryPage} />
    <Route path="alert" component={RedAlertPage} />
  </Route>
);
