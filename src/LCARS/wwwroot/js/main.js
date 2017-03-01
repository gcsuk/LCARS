import { app, router, h } from 'hyperapp';
import hyperx from 'hyperx';

const html = hyperx(h);

const model = {
  fetched: false,
  branches: []
};

const effects = {
  fetch(model, actions, { repo }) {
    const url = `/api/github/branches/${repo}`;

    if (!model.fetched) {
      fetch(url)
        .then(res => res.json())
        .then(res => actions.update({ branches: res, fetched: true }));
    }
  }
}

const reducers = {
  update: (model, { branches, fetched }) => ({ branches: branches || [], fetched })
}

const view = {
  '/': (model, actions) => html`<button onclick=${() => actions.setLocation('/github/branches/zeus')}> About</button>`,
  '/github/branches/:repo': (model, actions, key) => html`
    <div>
      ${model.fetched ? '' : actions.fetch(key)}
      ${model.branches.map(b => html`<div>name: ${b.name}</div>`)}
    </div>`
};

app({ model, view, reducers, effects, router });
