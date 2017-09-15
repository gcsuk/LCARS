import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import {issueSummary}  from '../../data/issues';
import yellowAlertStyles from '../../css/alertYellow.css'

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
  constructor(props) {
    super(props);

    this.state = {issueSummary};
  }
  render() {
    return (
      <IssueSummaryContainer>
          <div></div>
          <IssueSummaryIcon>
            <BugIcon src="../../img/softwareBug.jpg" />
          </IssueSummaryIcon>
          <IssueSummary>
              <div>
                {issueSummary.issueSet}: <span>{issueSummary.issueCount}</span>
              </div>
              <div>
                  Days Left: <span>{issueSummary.numberOfWorkingDays}</span>
              </div>
              <div>
                  ({issueSummary.numberOfWorkingHours} hours, {issueSummary.numberOfWorkingMinutes} minutes)
              </div>
          </IssueSummary>
          <IssueSummaryRight>
          </IssueSummaryRight>
      </IssueSummaryContainer>
    );
  }
}

export default IssueSummaryPage;
