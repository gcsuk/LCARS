import React from 'react';
import { Link, IndexLink } from 'react-router';

const Header = () => {
  return (
    <nav>
      <IndexLink to="/" activeClassName="active">Home</IndexLink>
      {" | "}
      <Link to="/builds" activeClassName="active">Builds</Link>
      {" | "}
      <Link to="/deployments" activeClassName="active">Deployments</Link>
      {" | "}
      <Link to="/environments" activeClassName="active">Environments</Link>
      {" | "}
      <Link to="/git" activeClassName="active">Git</Link>
      {" | "}
      <Link to="/issues" activeClassName="active">Issues</Link>
    </nav>
  );
};

export default Header;
