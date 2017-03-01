import { app, h } from 'hyperapp';
import hyperx from 'hyperx';

const html = hyperx(h);

const model = 0;

const reducers = {
  add: model => model + 1,
  sub: model => model - 1
};

const view = (model, actions) => html`
  <div>
  <button onclick=${actions.add}>+</button>
  <h1>${model}</h1>
  <button onclick=${actions.sub} disabled=${model <= 0}>-</button>
  </div>
`;

app({ model, view, reducers });
