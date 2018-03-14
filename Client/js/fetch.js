import fetch from 'unfetch';

const getData = (endpoint) => {
  return fetch(`http://en1bdess01dsh01.ad.bedegaming.com:8082/api/${endpoint}`)
          .then(res => res.json());
};

export default getData;