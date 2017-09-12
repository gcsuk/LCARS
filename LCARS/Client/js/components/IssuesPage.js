import React from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import issueData from '../../data/issues';

const Issues = styled.table`
  width: 100%;
  vertical-align: middle
`;

const IssuePriority = styled.td`
  text-align: right;
  vertical-align: middle;
  width: 80px
`;

const IssueNumber = styled.td`
  width: 150px;
  text-align: center;
  vertical-align: middle
`;

const IssueSummary = styled.td`
  vertical-align: middle
`;

const PriorityIcon = styled.img`
  height: 40px;
  border: solid 1px #fff;
  border-radius: 5px;
`;

class IssuesPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {issues: issueData};
  }
  render() {
    return (
      <Issues>
        <thead>
          <tr>
            <IssuePriority>Priority</IssuePriority>
            <IssueNumber>#</IssueNumber>
            <IssueSummary>Summary</IssueSummary>
          </tr>
        </thead>
        <tbody>
          { this.state.issues.map((issue, rowIndex) => (
            <tr key={rowIndex}>
              <IssuePriority><PriorityIcon src={issue.priorityIcon} alt={issue.priority} /></IssuePriority>
              <IssueNumber>{issue.id}</IssueNumber>
              <IssueSummary>{issue.summary}</IssueSummary>
            </tr>
          ))}
        </tbody>
      </Issues>
    );
  }
}

export default IssuesPage;
