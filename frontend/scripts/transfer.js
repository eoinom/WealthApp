const fs = require('fs-extra')
const fs2 = require('fs-extra')

const folder = process.argv[2]

async function execute() {
  try {
    await fs.emptyDir(`./public/${folder}`)
    console.log('Deleted public folder contents!')

    await fs.copy(`./dist/${folder}`, `./public/${folder}`, { overwrite: true })
    console.log('Installed new files in public folder!')

    fs.copy('./public/now.json', `./public/${folder}/now.json`, function(err){
      if (err) {
        return console.error(err);
      }    
      console.log('Copied now.json file to public folder!')
    });
  } catch (err) {
    console.error(err)
  }
}

execute()