// @flow
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../actions';

export function mapStateToProps(state: Object) {
  return {
    builds: state.builds,
    deployments: state.deployments,
    environments: state.environments,
    git: state.git,
    issues: state.issues,
    issueSummary: state.issueSummary,
    alertCondition: state.alertCondition
  };
}

export function mapDispatchToProps(dispatch: Dispatch) {
  return bindActionCreators(actionCreators, dispatch);
}