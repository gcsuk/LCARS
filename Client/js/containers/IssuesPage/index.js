import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';

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

class IssuesPage extends Component {
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
          { this.props.issues.map((issue, rowIndex) => (
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

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(IssuesPage);