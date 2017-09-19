import { delay } from 'redux-saga';

export function* buildsSaga () {
    while (true) {
        yield delay(1000);
        console.log("Build saga loop");
    }
}