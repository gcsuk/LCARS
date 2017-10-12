import React, {Component} from 'react';
import {Link, withRouter} from 'react-router-dom';
import Digits from '../../components/Digits';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';
import Routes from '../../components/Routes';

class  Layout extends Component {
  render() {
    return (
        <div className={`condition-${this.props.alertCondition.condition}`}>
            <header>
                <div id="header-left" className="left">
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
                    <span />
                    <span />
                    <span />
                    <span />
                </div>
            </header>
            <div className="body">
                <aside>
                    <div>
                        <span><Link to="/admin">CONFIG</Link></span>
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
                </aside>
                <main>
                    <div className="box-pattern">
                        <span />
                        <span />
                        <span />
                        <span />
                    </div>
                    <div className="content">
                        <Routes/>
                    </div>
                </main>
            </div>
        </div>
    );
  }
}

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(Layout));