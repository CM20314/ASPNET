# CM20314 - ASP.NET Web API

## Get started
1. [Download](https://visualstudio.microsoft.com/downloads/) Visual Studio 2022 (not Visual Studio Code). Ensure you enable everything related to ASP.NET when configuring Visual Studio.
2. [Download](https://git-scm.com/) `git`.
3. Go to `%USERPROFILE%\source\repos` and open the command line.
4. Run `git` to ensure git is installed correctly.
5. Run `git clone https://github.com/CM20314/ASPNET`.
6. Run `cd ASPNET`
6. Run `git checkout development`. **This is crucial.**

## Working on the project
**Please follow these steps *very carefully* to avoid unnecessary headaches.**
1. Before opening Visual Studio, go to `%USERPROFILE%\source\repos\CM20314` and open the command line.
2. Run `git checkout -b feature-[feature_name]`, repacing `[feature_name]` with your feature, e.g. `feature-dijkstra`.
3. Write the code, test it etc.
4. Run `git commit -a -m "[commit_name]"` replacing `[commit_name]` with your commit name, e.g. `fixed case for same start and destination`. If this tells you to add files, run `git add -A` then retry.
5. Run `git checkout development`. This moves your `HEAD` to the `development` branch.
6. Run `git pull` (if this doesn't work, `git pull origin development`) to pull changes from the cloud to your local copy so as not to overwrite others' work.
7. Run `git merge feature-[feature_name]`. This will merge your changes.
8. If someone else has made a change to the same file as you, there will be a merge conflict. To resolve this:
* Open the conflicted file and manually resolve the conflict, keeping the lines of code you want to keep.
* Once you've sorted them all out, run `git add -A` to add all of the files.
* Run `git merge --continue` to complete the merge.
9. Run `git commit -a -m "merged feature-[feature_name] into development"`.
10. If you aren't going to merge any more changes, update the remote copy by running `git push`.
11. Ideally, run `git branch -D feature-[feature_name]` to delete the feature branch if you are done with it. You won't need it any more.

## Branch information
1. Please be very careful when merging. Make sure you run `git pull` before you push local changes.
2. There are currently four branches:
* `development` is to develop the API. Create new branches for new features you are working on, then merge back to `development` when you are done.
* `alpha` is for alpha testing. This will (once I set it up) automatically build the app and release it to our Android devices using Microsoft AppCenter.
* `beta` is for beta testing. This will (once I set it up) automatically build the app and release it to our beta testers using Microsoft AppCenter.
* `master` is for production. Do not push to this branch before consulting the group.
3. **Every merge should follow the order `development > alpha > beta > master`.**