//@flow

import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';
import yellowAlertStyles from '../../../css/alertYellow.css'

const IssueSummaryContainer = styled.div`
  display: grid;
  grid-template-columns: auto 300px 500px auto
`;

const IssueSummaryIcon = styled.div`
  grid-column: 2;
  padding-top: 5%
`;

const BugIcon = styled.img`
  width: 90%;
  vertical-align: middle
`;

const IssueSummary = styled.div`
  grid-column: 3;
  font-size: 48pt
`;

const IssueSummaryRight = styled.div`
  grid-column: 4
`;

class IssueSummaryPage extends Component {
  render() {
    return (
      <IssueSummaryContainer>
          <div></div>
          <IssueSummaryIcon>
            <BugIcon src="../../img/softwareBug.jpg" />
          </IssueSummaryIcon>
          <IssueSummary>
              <div>
                {this.props.issueSummary.issueSet}: <span>{this.props.issueSummary.issueCount}</span>
              </div>
              <div>
                  Days Left: <span>{this.props.issueSummary.numberOfWorkingDays}</span>
              </div>
              <div>
                  ({this.props.issueSummary.numberOfWorkingHours} hours, {this.props.issueSummary.numberOfWorkingMinutes} minutes)
              </div>
          </IssueSummary>
          <IssueSummaryRight>
          </IssueSummaryRight>
      </IssueSummaryContainer>
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(IssueSummaryPage);