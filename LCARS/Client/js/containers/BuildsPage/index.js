import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';

const Builds = styled.div`
  text-align: center;
`;

const Build = styled.div`
  margin: 10px 0
`;

const ProjectName = styled.span`
  display: inline-block;
  width: 300px;
  text-align: left
`;

const BuildStatus = styled.span`
  display: inline-block;
  padding: 3px 35px;
  font-size: 30px;
  border-radius: 20px;
  background-color: #015001
`;

class BuildsPage extends Component { 
  render() {
    return (
      <Builds>
        { this.props.builds.map((project, rowIndex) => (
          <Build key={rowIndex}>
            <ProjectName row={rowIndex}>
              {project.name}
            </ProjectName>
            <BuildStatus row={rowIndex}>
              {project.status} - {project.version}
            </BuildStatus>
          </Build>)
        )}
      </Builds>
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(BuildsPage);