import React, {Component} from 'react';
import {Link} from 'react-router-dom';
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
  font-size: 1.8rem;
  text-align: left
`;

const BuildStatus = styled.span`
  display: inline-block;
  padding: 3px 35px;
  width: 250px;
  font-size: 1.8rem;
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