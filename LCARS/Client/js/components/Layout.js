import React from 'react';
import {Link} from 'react-router';
import Digits from './Digits';

class  Layout extends React.Component {
  render() {
    return (
        <div className="container">
            <div className="header">
                <div className="left">
                    21-08-2017
                </div>
                <div className="middle">
                    <Digits rowCount={6} colCount={13} />
                </div>
                <div className="right">
                    <div className="title">
                        LCARS OPERATIONS <span>ONLINE</span>
                    </div>
                    <div className="buttons">
                        <div>07-3215</div>
                        <div>08-5012</div>
                        <div>09-3123</div>
                        <div>10-2415</div>
                    </div>
                </div>
                <div className="box-pattern">
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
            <div className="body">
                <div className="sidebar">
                    <div>
                        <span>CONFIG</span>
                    </div>
                    <div>
                        <span>STATUS</span>
                    </div>
                    <div>
                        <span>05-32456</span>
                    </div>
                    <div>
                        <span>06-15868</span>
                    </div>
                </div>
                <div className="main">
                    <div className="box-pattern">
                        <span></span>
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>
                    <div className="content">
                        {this.props.children}
                    </div>
                </div>
            </div>
        </div>
    );
  }
}

export default Layout;