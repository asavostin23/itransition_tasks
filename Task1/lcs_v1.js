if (process.argv.length < 3) {
    console.log();
}
else if (process.argv.length == 3) {
    console.log(process.argv[2]);
}
else {
    let subs = [''];
    let fstr = process.argv[2];
    let sstr = process.argv[3];
    for (let i = 0; i < fstr.length; i++) {
        let j = 0;
        while (sstr[j] != fstr[i] && j < sstr.length) {
            j++;
        }
        let it = i;
        while (fstr[it] == sstr[j] && it < fstr.length && j < sstr.length) {
            it++;
            j++;
            if (i < it) {//
                subs.push(fstr.slice(i, it));
            }
        }
    }
    subs = subs.filter(el => process.argv.slice(4).every(arg => arg.includes(el)));
    let mxsub = subs[0];
    for (str of subs) {
        if (mxsub.length < str.length)
            mxsub = str;
    }
    console.log(mxsub);
}