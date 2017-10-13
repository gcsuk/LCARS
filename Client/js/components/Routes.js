import React from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import BuildsPage from '../containers/BuildsPage';
import DeploymentsPage from '../containers/DeploymentsPage';
import GitPage from '../containers/GitPage';
import EnvironmentsPage from '../containers/EnvironmentsPage';
import IssuesPage from '../containers/IssuesPage';
import IssueSummaryPage from '../containers/IssueSummaryPage';
import RedAlertPage from '../containers/RedAlertPage';

const Routes = () => (
  <Switch>
    <Route exact path="/builds" component={BuildsPage} />
    <Route exact path="/deployments" component={DeploymentsPage} />
    <Route exact path="/git" component={GitPage} />
    <Route exact path="/environments" component={EnvironmentsPage} />
    <Route exact path="/issues" component={IssuesPage} />
    <Route exact path="/issuesummary" component={IssueSummaryPage} />
    <Route exact path="/alert" component={RedAlertPage} />
  </Switch>
);

export default withRouter(Routes);