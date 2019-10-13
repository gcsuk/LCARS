import React from 'react';
import Digits from './components/Digits';
import Today from './components/Today';
import './App.css';

function App() {
  return (
    <div className={`condition-green`}>
      <header>
          <div id="header-left" className="left">
              <Today />
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
          </aside>
          <main>
              <div className="box-pattern">
                  <span />
                  <span />
                  <span />
                  <span />
              </div>
              <div className="content">
              </div>
          </main>
      </div>
    </div>
  );
}

export default App;